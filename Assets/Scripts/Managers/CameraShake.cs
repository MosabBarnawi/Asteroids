using System.Collections;
using UnityEngine;

/// <summary>
/// Util For Camera Shaking
/// </summary>
public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 0.2f;
    [SerializeField] Vector2 frequency = Vector2.one;

    private Transform cameraTransform;
    private Vector3 originalPosition;

    #region Public Methods
    public void SetUpCameraTransform(Camera camera)
    {
        cameraTransform = camera.transform;
        originalPosition = cameraTransform.localPosition;
    }
    public void ShakeCamera() => StartCoroutine(Shake());
    #endregion
    private IEnumerator Shake()
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            Vector2 shakePosition = new Vector3(
                Random.Range(-frequency.x, frequency.x),
                cameraTransform.position.y,
                Random.Range(-frequency.y, frequency.y));

            cameraTransform.localPosition = shakePosition;

            timeElapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }
}