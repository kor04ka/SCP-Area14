using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(InteractionMarkEnablerDisabler))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] LayerMask m_itemsLayerMask;
    [SerializeField] float m_maxInteractionDistance;
    [SerializeField] float m_radiousOfSphereInteraction;
    [SerializeField] float m_delayAfterInteraction;

    [Inject] readonly RayProvider m_rayProvider;
    [Inject] readonly InventoryEnablerDisabler m_inventoryEnablerDisabler;
    [Inject] readonly GameObject m_playerGameObject;

    IInteractable m_interactable;
    bool m_isDelayGoing;
    WaitForSeconds m_timeoutAfterInteraction;

    public Action OnPlayerFindUnInteractable { get; set; }
    public Action<Collider> OnPlayerFindInteractable { get; set; }

    void Start()
    {
        m_inventoryEnablerDisabler.OnInventoryEnabledDisabled += SetActiveState;
        m_timeoutAfterInteraction = new WaitForSeconds(m_delayAfterInteraction);
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    void Update()
    {
        if (m_isDelayGoing) { return; }

        RaycastHit[] raycastHits = Physics.SphereCastAll(m_rayProvider.ProvideRay(),
                                                         m_radiousOfSphereInteraction,
                                                         m_maxInteractionDistance,
                                                         m_itemsLayerMask);

        Collider raycastHit = GetInteractableObject(raycastHits);

        if (raycastHit == null)
        {
            OnPlayerFindUnInteractable?.Invoke();
            return;
        }

        OnPlayerFindInteractable?.Invoke(raycastHit);

        if (!Input.GetButtonDown("Interaction")) { return; }

        StartCoroutine(StartInteractionDelay());
        m_interactable.Interact();
    }

    Collider GetInteractableObject(RaycastHit[] raycastHits)
    {
        if (raycastHits == null)
        {
            return null;
        }

        return raycastHits.LastOrDefault(hit => hit.collider.gameObject.TryGetComponent(out m_interactable)).collider;
    }

    IEnumerator StartInteractionDelay()
    {
        OnPlayerFindUnInteractable?.Invoke();
        m_isDelayGoing = true;

        yield return m_timeoutAfterInteraction;

        m_isDelayGoing = false;
    }

    void OnDestroy()
    {
        m_inventoryEnablerDisabler.OnInventoryEnabledDisabled -= SetActiveState;
    }

}
