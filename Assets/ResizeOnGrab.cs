using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeOnGrab : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject leftHandPos;
  public GameObject rightHandPos;
  public Text debugText;
  private OVRGrabbable ovrGrabbable;
  private Transform objTransform;
  private Vector3 initial_scale;
  private float low_limit_rel = 0.2f;
  private float upper_limit_rel = 3.0f;
  private float low_limit;
  private float upper_limit;

  private	OVRHapticsClip clipLight;
private	OVRHapticsClip clipMedium;
private	OVRHapticsClip clipHard;

  private float scale_currentDistance;
  private float scale_previousDistance;
  private bool is_scaled;
  void Start()
  {
    InitializeOVRHaptics();
    ovrGrabbable = GetComponent<OVRGrabbable>();
    initial_scale = GetComponent<Transform>().localScale;
    low_limit = low_limit_rel * initial_scale.x;
    upper_limit = upper_limit_rel* initial_scale.x;
    scale_currentDistance = 0;
    scale_previousDistance = -1;
    objTransform = GetComponent<Transform>();
    is_scaled = false;
    // leftHandPos = GameObject.Find("/OVRPlayerController/OVRCameraRig/TrackingSpace/LeftHandAnchor");
    // rightHandPos = GameObject.Find("/OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor");
  }

  // Update is called once per frame
  void Update()
  {
    bool is_grab_one = OVRInput.Get(OVRInput.RawButton.LHandTrigger, OVRInput.Controller.LTouch);
    bool is_grab_two = OVRInput.Get(OVRInput.RawButton.RHandTrigger, OVRInput.Controller.RTouch);
    //scale_previousDistance = -1;
    //is_scaled = false;
    Vector3 initial_left_hand_pos = leftHandPos.GetComponent<Transform>().position;
    Vector3 initial_right_hand_pos = rightHandPos.GetComponent<Transform>().position;
    scale_currentDistance = Vector3.Distance(initial_left_hand_pos, initial_right_hand_pos);

    bool obj_grabbed_L = leftHandPos.GetComponentInChildren<OVRGrabber>().grabbedObject != null;
    bool obj_grabbed_R = rightHandPos.GetComponentInChildren<OVRGrabber>().grabbedObject != null;
    bool is_grabbing_one = !(obj_grabbed_L && obj_grabbed_R);

    // Vector3 initialHandPosition1 = initial_left_hand_pos;
    // Vector3 initialHandPosition2 = initial_right_hand_pos;
    // Quaternion initialObjectRotation = ovrGrabbable.transform.rotation;
    // Vector3 initialObjectScale = ovrGrabbable.transform.localScale; 
    // Vector3 initialObjectDirection = ovrGrabbable.transform.position - (initialHandPosition1 + initialHandPosition2) * 0.5f;
    
    if (ovrGrabbable.isGrabbed && is_grab_one && is_grab_two && is_grabbing_one)
    {
     
      OVRInput.Controller grab_controller = ovrGrabbable.grabbedBy.GetController();
      OVRInput.Controller other_controller = grab_controller == OVRInput.Controller.LTouch ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;


      //if (System.Math.Abs(scale_previousDistance) > 1e-3)
      // if (scale_previousDistance > 1e-2)
    //   if (scale_previousDistance > 5e-2)
    // Vector3 currentHandPosition1 = leftHandPos.GetComponent<Transform>().position; // current first hand position
    // Vector3 currentHandPosition2 = rightHandPos.GetComponent<Transform>().position; // current second hand position
		// Vector3 handDir1 = (initialHandPosition1 - initialHandPosition2).normalized; // direction vector of initial first and second hand position
    // Vector3 handDir2 = (currentHandPosition1 - currentHandPosition2).normalized; // direction vector of current first and second hand position 
    // Quaternion handRot = Quaternion.FromToRotation(handDir1, handDir2); // calculate rotation based on those two direction vectors
    //  float currentGrabDistance = Vector3.Distance(currentHandPosition1, currentHandPosition2);
    //   float initialGrabDistance = Vector3.Distance(initialHandPosition1, initialHandPosition2);
    // float p = (currentGrabDistance / initialGrabDistance); // percentage based on the distance of the initial positions and the new positions
    //  Vector3 newScale = new Vector3(p * initialObjectScale.x, p * initialObjectScale.y, p * initialObjectScale.z); // calculate new object scale with p
    // ovrGrabbable.transform.rotation = handRot * initialObjectRotation; // add rotation
    // ovrGrabbable.transform.localScale = newScale; // set new scale
        // set the position of the object to the center of both hands based on the original object direction relative to the new scale and rotation
      // ovrGrabbable.transform.position = (0.5f * (currentHandPosition1 + currentHandPosition2)) + (handRot * (initialObjectDirection * p));


      if (scale_previousDistance > 1e-2)
      {
        //  Vector3 pivot_point;
        // if (grab_controller == OVRInput.Controller.LTouch)
        // {
        //   pivot_point = initial_left_hand_pos;
        // }
        // else
        // {
        //   pivot_point = initial_right_hand_pos;
        // }

        // pivot_point = ovrGrabbable.grabPoints[0].gameObject.transform.position;
        GameObject obj_dummy = new GameObject();
        Transform pivot_point = obj_dummy.transform;
        if (grab_controller == OVRInput.Controller.LTouch)
        {
          pivot_point.position = leftHandPos.transform.position;
        }
        else
        {
          pivot_point.position = rightHandPos.transform.position;
        }

        // pivot_point.position = ovrGrabbable.grabPoints[0].transform.position;
        // Vector3 save_pos = pivot_point.position;

        float scale_amount = scale_currentDistance - scale_previousDistance;
        scale_amount /= scale_currentDistance;
        scale_amount *= 0.75f;
        // if (debugText != null)
        // {
        //   debugText.text = scale_previousDistance.ToString();
        //   debugText.text += "\n";
        //   debugText.text += scale_currentDistance.ToString();
        // }
        //Vector3 new_scale = new Vector3 (scale_amount, scale_amount, scale_amount);

        //new_scale *= upper_limit;
        //new_scale += objTransform.localScale;
        Vector3 new_scale = objTransform.localScale + scale_amount * objTransform.localScale;
        // new_scale = newScale;
        objTransform.localScale = new_scale;

        // ScaleAround(objTransform, pivot_point, new_scale);
        
        if (new_scale.x > upper_limit)
        {
          objTransform.localScale = new Vector3(upper_limit, upper_limit, upper_limit);
          // Vector3 max_scale = new Vector3(upper_limit, upper_limit, upper_limit);
          // ScaleAround(objTransform, pivot_point, max_scale);

        }
        else if (new_scale.x < low_limit)
        {
          objTransform.localScale = new Vector3(low_limit, low_limit, low_limit);
          // Vector3 min_scale = new Vector3(low_limit, low_limit, low_limit);
          // ScaleAround(objTransform, pivot_point, min_scale);

        }
        
        // objTransform.position = save_pos;
        float vibe_ratio = objTransform.localScale.x / initial_scale.x;
        vibe_ratio = Remap(vibe_ratio, low_limit_rel, upper_limit_rel, 75, 255);

        if(grab_controller == OVRInput.Controller.RTouch) {

             OVRHaptics.RightChannel.Preempt(clipMedium);
             OVRHaptics.LeftChannel.Preempt(GetOVRHaptic((int)vibe_ratio));
        } else {
            OVRHaptics.RightChannel.Preempt(GetOVRHaptic((int)vibe_ratio));
            OVRHaptics.LeftChannel.Preempt(clipMedium);
        }
        // int my_vibration_freq = (int)(scale_amount * 10);
        //VIBRATION
        // OVRInput.SetControllerVibration(0.5f, 0.4f, grab_controller);
        // OVRInput.SetControllerVibration(my_vibration_freq, 0.8f, other_controller);
        // TriggerVibration(500, 1, 255, grab_controller); //VALEM VIDEO
        // TriggerVibration(500, 1, 255, other_controller);
        // is_scaled = true;
      }
      // if (!is_scaled) {
      //     scale_previousDistance = scale_currentDistance;
      // } else {
      //     is_scaled = false;
      //     scale_previousDistance = -1;
      // }
    }
    scale_previousDistance = scale_currentDistance;
  }

  private float Remap(float value, float from1, float to1, float from2, float to2)
  {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
  }

  // private void ScaleAround(Transform target, Vector3 pivot, Vector3 newScale)
  // {
  //   Vector3 A = target.localPosition;
  //   Vector3 B = pivot;

  //   Vector3 C = A - B; // diff from object pivot to desired pivot/origin

  //   float RS = newScale.x / target.localScale.x; // relataive scale factor

  //   // calc final position post-scale
  //   Vector3 FP = B + C * RS;

  //   // finally, actually perform the scale/translation
  //   target.localScale = newScale;
  //   target.localPosition = FP;
  // }

   private void ScaleAround(Transform target, Transform pivot, Vector3 scale) {
         Transform pivotParent = pivot.parent;
         Vector3 pivotPos = pivot.position;
         pivot.parent = target;        
         target.localScale = scale;
         target.position += pivotPos - pivot.position;
         pivot.parent = pivotParent;
     }


  public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
  {
    OVRHapticsClip clip = new OVRHapticsClip();

    for (int i = 0; i < iteration; i++)
    {
      clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
    }

    if (controller == OVRInput.Controller.LTouch)
    {
      OVRHaptics.LeftChannel.Preempt(clip);
    }
    else if (controller == OVRInput.Controller.RTouch)
    {
      OVRHaptics.RightChannel.Preempt(clip);
    }
  }

private void InitializeOVRHaptics()
	{
		int cnt = 20;
		clipLight = new OVRHapticsClip(cnt);
		clipMedium = new OVRHapticsClip(cnt);
		clipHard = new OVRHapticsClip(cnt);
		for (int i = 0; i < cnt; i++)
		{
			clipLight.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)75;
			clipMedium.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)150;
			clipHard.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)255;
		}

		clipLight = new OVRHapticsClip(clipLight.Samples, clipLight.Samples.Length);
		clipMedium = new OVRHapticsClip(clipMedium.Samples, clipMedium.Samples.Length);
		clipHard = new OVRHapticsClip(clipHard.Samples, clipHard.Samples.Length);
	}
private OVRHapticsClip GetOVRHaptic(int strength, int length = 10, int frequency = 2)
  {
    OVRHapticsClip _clip = new OVRHapticsClip(length);
    for (int i = 0; i < length; i++)
    {
      _clip.Samples[i] = i % frequency == 0 ? (byte)0 : (byte)strength;

    }
    _clip = new OVRHapticsClip(_clip.Samples, _clip.Samples.Length);
    return _clip;
  }

}

//  public static void ScaleAround(Transform target, Transform pivot, Vector3 scale) {
//          Transform pivotParent = pivot.parent;
//          Vector3 pivotPos = pivot.position;
//          pivot.parent = target;        
//          target.localScale = scale;
//          target.position += pivotPos - pivot.position;
//          pivot.parent = pivotParent;
//      }


