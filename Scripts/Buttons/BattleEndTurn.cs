using UnityEngine;

public class BattleEndTurn : MonoBehaviour
{
    public void OnEndTurnClicked() => BattleFacade.Instance.EndTurn();
}