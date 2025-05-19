using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SojaExiles
{

    public class keyOpenClose : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

        
        GameObject playerObj;

        public bool unlocked = false;

        private PlayerInventory inventory;

        

        void Start()
        {
            open = false;
            playerObj = GameObject.FindGameObjectWithTag("Player");
            inventory = playerObj.GetComponent<PlayerInventory>();
        }

        void OnMouseOver()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15 && Input.GetMouseButtonDown(0))
                {
                    if (!unlocked)
                    {
                        print("asdf");
                        if (inventory.hasKey)
                        {
                            unlocked = true;

                        }
                        else
                        {
                            SubtitleManager.Instance.ShowSubtitle("you need a key", 3f);
                        }
                    }
                    else
                    {
                        if (!open)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
            }
        }

       

        IEnumerator opening()
        {
            print("you are opening the door");
            SubtitleManager.Instance.ShowSubtitle("you are opening the door...", 3f);

            openandclose.Play("Opening");
            open = true;
            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the door");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(.5f);
        }

        
    }
}
