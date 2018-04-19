﻿using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Items
{
    public abstract class AbstractItem : MonoBehaviour
    {
        // Self-Destruct if we moved out of the camera view
        public virtual void LateUpdate()
        {            
            if (gameObject.transform.position.x <= Camera.main.transform.position.x - (App.Cache.cameraWidth / 2f))
            {
                gameObject.SetActive(false);
                GameObject.Destroy(gameObject);
            }
        }     
        

        protected bool TryGetBody(Collider2D collision, out Rigidbody2D body)
        {
            body = collision.gameObject.GetComponent<Rigidbody2D>();

            return body != null;
        }


        protected bool TryGetJester(Collider2D collision, out Jester jester)
        {
            jester = collision.gameObject.GetComponent<Jester>();

            return jester != null;
        }
    }
}