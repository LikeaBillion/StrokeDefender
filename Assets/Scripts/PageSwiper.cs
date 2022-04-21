using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{   
    //vector for keeping hold of the current location of the panel
    private Vector3 panelLocation;
    //tuning of the 'snap' effect
    public float percentThreashold = 0.2f;
    public float easing = 0.5f;
    //variables for totalpages overall, and the current page
    private int totalPages = 2;
    private int currentPage = 1;

    // Start is called before the first frame update
    void Start()
    {
        //panel location is = to its current location
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data){
        //differnce is calculated- how much the user moves the pointer
        float difference = data.pressPosition.x - data.position.x;
        //sets the panel to old panellocation - difference in the x
        transform.position = panelLocation - new Vector3(difference,0,0);
    }

    public void OnEndDrag(PointerEventData data){
        //calculates dragged across
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        //if percentage is bigger than the threshold set
        if(Mathf.Abs(percentage) >= percentThreashold){
            //set the newLocation to be the panelslocation
            Vector3 newLocation = panelLocation;
            //works out direction and if there is a panel there to move to
            if(percentage > 0 && currentPage < totalPages){
                currentPage ++;
                newLocation += new Vector3(-Screen.width,0,0);
            }
            //works out direction and if there is a panel there to move to
            else if(percentage < 0 && currentPage > 1){
                currentPage --;
                newLocation += new Vector3(Screen.width,0,0);
            }
            //coroutine to make the movement smooth and not jaring 
            StartCoroutine(SmoothMove(transform.position,newLocation,easing));
            //sets the neLocation to be the panel location
            panelLocation = newLocation;
        }
        else{
            //return to the origonal position smoothly
            StartCoroutine(SmoothMove(transform.position,panelLocation,easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos,float seconds){
        //starts recording time
        float t = 0f;
        //while t is smaller than 1
        while(t <= 1){
            t += Time.deltaTime/seconds;
            //slowly transform the page back towards postition stated
            transform.position = Vector3.Lerp(startpos,endpos,Mathf.SmoothStep(0,1f,t));
            yield return null;
        }
    }
}
