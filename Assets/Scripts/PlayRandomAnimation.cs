using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator[] _animators;

    private float[] _randomTimes;

    [SerializeField]
    private float _randomIntervalMin = 5f;

    [SerializeField]
    private float _randomIntervalMax = 20f;

    void Start()
    {
        _randomTimes = new float[_animators.Length];

        for (int i = 0; i < _animators.Length; i++)
        {
            int randomInt = Random.Range(0, 4);
            _animators[i].SetInteger("NextAnimation", randomInt);
            float randomFloat = Random.Range(_randomIntervalMin, _randomIntervalMax);
            _randomTimes[i] = Time.realtimeSinceStartup + randomFloat;
        }

        StartCoroutine(UpdateAnimators());
    }

    private IEnumerator UpdateAnimators()
    {
        while (true)
        {
            for (int i = 0; i < _randomTimes.Length; i++)
            {
                if (Time.realtimeSinceStartup > _randomTimes[i])
                {
                    int randomInt = Random.Range(0, 4);
                    float randomFloat = Random.Range(_randomIntervalMin, _randomIntervalMax);
                    _animators[i].SetInteger("NextAnimationIndex", randomInt);
                    _randomTimes[i] = Time.realtimeSinceStartup + randomFloat;

                    Debug.Log("update " + i + "   " + Time.realtimeSinceStartup);
                }
            }
            yield return null;
        }
    }
}
