using System.Collections;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    Animator animator;
    //projectile gameobject 추가
    //projectile pooling 추가

    float projectileSpeed, atkSpeed;//player로부터 받아옴.
    bool isFire, isStatusChange;
    Coroutine fireRoutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Fire();
    }
    void Update()
    {
        if (isFire)
        {
            ;
            //월드 좌표 기준으로 회전 적용
            //transform.rotation = MouseManager.Instance.GetRotationInfo() * Quaternion.Inverse(transform.parent.rotation);
            //transform.rotation = MouseManager.Instance.GetRotationInfo();
            // 투사체 생성 코드
        }
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            isFire = true;
            animator.SetBool("isFire", isFire);
            yield return new WaitForSeconds(atkSpeed);
            isFire = false;
            animator.SetBool("isFire", isFire);
        }
    }
    public void Fire()
    {
        fireRoutine ??= StartCoroutine(FireRoutine());//fireRoutine이 null일때 실행
    }
    public void RestartFireRoutine()
    {
        isStatusChange = false;
        StopCoroutine(fireRoutine);
        fireRoutine = null;
        Fire();
    }
    public void SetProjectileSpeed(float speed)
    {
        projectileSpeed = speed;
        isStatusChange = true;
        RestartFireRoutine();
    }

    public void SetAtkSpeed(float speed)
    {
        atkSpeed = speed;
        isStatusChange = true;
        RestartFireRoutine();
    }
}
