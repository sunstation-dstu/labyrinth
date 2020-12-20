using System.Collections;
using UnityEngine;

public class HPCount : MonoBehaviour
{
    [Range(0,100)]
    public int hp = 100;
    public int deathDuration;
    public bool haveSelfHealthing;
    public int selfHealthingPoints;
    public GameObject character;
    
    
    private DissolvedMaterial material;
    // Костыль в связи с нехваткой времени - конечно же тут надо было реализовать обычное наследование от
    // абстрактного класса Персонаж, например. Там и объявить абстракные классы смерти.
    // Так можно было бы сделать только одну перменную с одним типом. Наследование круто :(
    private Player playerBehaviourComponent;
    private Enemy enemyBehaviourComponent;
    private float t;
    private bool isplayerBehaviourComponentNotNull;
    private bool isenemyBehaviourComponentNotNull;

    void Start()
    {
        material = GetComponent<DissolvedMaterial>();
        playerBehaviourComponent = character.GetComponent<Player>();
        isplayerBehaviourComponentNotNull = playerBehaviourComponent != null;
        enemyBehaviourComponent = character.GetComponent<Enemy>();
        isenemyBehaviourComponentNotNull = enemyBehaviourComponent != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            return;
        }

        hp = 0;

        if (Mathf.Abs(1 - material.dissolveAmount) < 0.01)
        {
            if (isplayerBehaviourComponentNotNull)
            {
                playerBehaviourComponent.Death();
            }
            else if (isenemyBehaviourComponentNotNull)
            {
                enemyBehaviourComponent.Death();
            }
            return;
        }

        t += Time.deltaTime / deathDuration;
        material.dissolveAmount = Mathf.Lerp(0, 1, t);
    }
}
