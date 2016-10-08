using UnityEngine;
using System.Collections;

public class SunEmotion
{
    float smiling, angryEyes, sad, closed, pursed, angry;

    public SunEmotion(float n1, float n2, float n3, float n4, float n5, float n6)
    {
        smiling = n1;
        angryEyes = n2;
        sad = n3;
        closed = n4;
        pursed = n5;
        angry = n6;
    }

    public float getSmiling()
    {
        return smiling;
    }

    public void setSmiling(float n)
    {
        smiling = n;
    }

    public float getAngryEyes() {
        return angryEyes;
    }

    public void setAngryEyes(float n) {
        angryEyes = n;
    }

    public float getSad() {
        return sad;
    }

    public void setSad(float n) {
        sad = n;
    }

    public float getClosed()
    {
        return closed;
    }

    public void setClosed(float n) {
        closed = n;
    }

    public float getPursed()
    {
        return pursed;
    }

    public void setPursed(float n) {
        pursed = n;
    }

    public float getAngry()
    {
        return angry;
    }

    public void setAngry(float n) {
        angry = n;
    }
}
