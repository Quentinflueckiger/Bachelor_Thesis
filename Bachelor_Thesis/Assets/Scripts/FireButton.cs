using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // designed to work in a pair with another axis touch button
        // (typically with one having -1 and one having 1 axisValues)
        public string axisName = "Fire"; // The name of the axis
        public float axisValue = 1; // The axis that the value has
        public float responseSpeed = 3; // The speed at which the axis touch button responds
        public float returnToCentreSpeed = 3; // The speed at which the button will return to its centre

        CrossPlatformInputManager.VirtualAxis m_Axis; // A reference to the virtual axis as it is in the cross platform input

        void OnEnable()
        {
            if (!CrossPlatformInputManager.AxisExists(axisName))
            {
                // if the axis doesnt exist create a new one in cross platform input
                m_Axis = new CrossPlatformInputManager.VirtualAxis(axisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_Axis);
            }
            else
            {
                m_Axis = CrossPlatformInputManager.VirtualAxisReference(axisName);
            }
           
        }



        void OnDisable()
        {
            // The object is disabled so remove it from the cross platform input system
            m_Axis.Remove();
        }


        public void OnPointerDown(PointerEventData data)
        {

            // update the axis and record that the button has been pressed this frame
            m_Axis.Update(1);
        }


        public void OnPointerUp(PointerEventData data)
        {
            m_Axis.Update(0);
        }
    }
}