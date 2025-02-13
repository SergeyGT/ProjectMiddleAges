using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    private KnifeController _knifeController;
    protected override void Start()
    {
        base.Start();

        _knifeController = FindObjectOfType<KnifeController>();

        transform.position = _knifeController.transform.position;
        Direction = _knifeController.PlayerMovement.lastMovedVector;
    }

    private void Update()
    {
        transform.position += Direction * _knifeController.Speed * Time.deltaTime;
    }

    protected void SetDirection()
    {
        float dirX = Direction.x;
        float dirY = Direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;
                                                                /* Снизу очень плохой код !!НЕ РАБОТАЕТ*/
                                            /* Для готового префаба тут надо подгонять угол и размер свой */

        //Установка размеров
        if (dirX < 0 && dirY == 0) //left
        {
            scale.x*=-1; 
            scale.y*=-1;
        }
        else if (dirX==0 && dirY<0) //down
        {
            scale.y *= -1;
        }
        else if(dirX==0 && dirY>0) //up 
        {
            scale.x *= -1;
        }

        else if (dirX > 0 && dirY > 0 ) //right up
        {
            rotation.z = 0;
        }
        else if (dirX > 0 && dirY < 0) //right down
        {
            rotation.z = -90;
        }
        else if (dirX < 0 && dirY > 0) //left up
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z *= -90;
        }
        else if (dirX < 0 && dirY < 0) //left down
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z *= 0;
        }


        transform.localScale = scale;   
        transform.rotation = Quaternion.Euler(rotation);
    }

}
