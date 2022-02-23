using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UtilityAgent : Agent
{

    [SerializeField] Perception perception;
    [SerializeField] MeterUI meter;

    Need[] needs;
    UtilityObject activeUitlityObject = null;

    public float happiness
    {
        get
        {
            float totalMotive = 0;
            foreach(var need in needs)
            {
                totalMotive += need.motive;
            }
            return 1 - (totalMotive / needs.Length);
        }
    }

    void Start()
    {
        needs = GetComponentsInChildren<Need>();
        meter.text.text = "";
    }

    void Update()
    {
        animator.SetFloat("speed", movement.velocity.magnitude);

        if(activeUitlityObject == null)
        {
            var gameObjects = perception.GetGameObjects();
            List<UtilityObject> utilityObjects = new List<UtilityObject>();
            foreach(var go in gameObjects)
            {
                if(go.TryGetComponent<UtilityObject>(out UtilityObject uo))
                {
                    utilityObjects.Add(uo);
                    uo.visible = true;
                    uo.score = GetUtilityObjectScore(uo);
                }
            }
        }
    }

    void LateUpdate()
    {
        meter.slider.value = happiness;
        meter.worldPosition = transform.position + Vector3.up * 4;
    }

    float GetUtilityObjectScore(UtilityObject uO)
    {
        float score = 0;

        foreach(var effector in uO.effectors)
        {
            Need need = GetNeedByType(effector.type);
            if(need != null)
            {
                float futureNeed = need.getMotive(need.input + effector.change);
                score += need.motive - futureNeed;
            }
        }

        return score;
    }

    Need GetNeedByType(Need.Type type)
    {
        return needs.First(need => need.type == type);
    }

    /*private void OnGUI()
    {
        Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);

        GUI.color = Color.black;
        int offset = 0;
        foreach (var need in needs)
        {
            GUI.Label(new Rect(screen.x + 20, Screen.height - screen.y - offset, 300, 20), need.type.ToString() + ": " + need.motive);
            offset += 20;
        }
        //GUI.Label(new Rect(screen.x + 20, Screen.height - screen.y - offset, 300, 20), mood.ToString());
    }*/
}
