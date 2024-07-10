using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;  // Time it takes to complete one orbit
    [SerializeField] float cooldown = 0.5f;
    public bool isCooldownActive = false;

    public IEnumerator RotateObject()
    {
        isCooldownActive = true;
        float elapsedTime = 0f;
        float startRotation = transform.rotation.eulerAngles.y;
        float endRotation = startRotation + 360f;
        

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration);  // Interpolation de l'angle de rotation
            transform.rotation = Quaternion.Euler(0, yRotation, 0);  // Appliquer la nouvelle rotation
            yield return null;  // Attendre le prochain frame
        }

        // Assurer que l'angle de rotation final est exactement de 360 degrés
        transform.rotation = Quaternion.Euler(0, endRotation, 0);
        yield return new WaitForSeconds(cooldown);

        // Cooldown is over
        isCooldownActive = false;
    }
    
    public bool CanRotate()
    {
        return !isCooldownActive;
    }
}
