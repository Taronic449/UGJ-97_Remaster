using System.Collections;
using Lean.Pool;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour,IPoolable
{
    public float shakeDuration = 0.5f;
    public float shakeAmount = 10f;
    public float growDuration = 0.5f;
    public float shrinkDuration = 0.5f;
    public float disappearDelay = 1f;

    private TextMeshPro textMesh;
    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;
    public ushort type;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        if (textMesh == null)
        {
            Debug.LogError("TextMeshPro component not found!");
        }
    }

    void IPoolable.OnSpawn()
    {
        textMesh.color = new Color(1,1,1);
    }

    void IPoolable.OnDespawn()
    {
    }

    public void Initialize(int damage)
    {
        // textMesh.text = "-" + damage.ToString();
        textMesh.text = Mathf.Abs(damage).ToString();

        if(damage == 0)
        {   
            type = 0;
        }
        else if(damage > 0)
        {
            type = 1;
        }
        else
        {
            type = 2;
        }
        

        textMesh.ForceMeshUpdate();
        textInfo = textMesh.textInfo;

        // Store the original vertices positions
        originalVertices = new Vector3[textInfo.meshInfo.Length][];
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] = new Vector3[textInfo.meshInfo[i].vertices.Length];
            System.Array.Copy(textInfo.meshInfo[i].vertices, originalVertices[i], textInfo.meshInfo[i].vertices.Length);
        }

        StartCoroutine(AnimateDamageNumber());
    }

    private IEnumerator AnimateDamageNumber()
    {
        if(type == 2)
        {
            shakeDuration = 0.5f;
            shakeAmount = 0.2f;
        }
        else
        {
            shakeAmount = 0.04f;
            shakeDuration = 0.5f;
        }

        StartCoroutine(ShakeCharacters());

        yield return StartCoroutine(GrowNumber());

        switch (type)
        {
            case 0:
                textMesh.color = new Color(0,0,0);
            break;

            case 1:
                textMesh.color = new Color(0,1,0);
            break;

            case 2:
                textMesh.color = new Color(1,0,0);
            break;

            default:
            break;
        }
        

        yield return new WaitForSeconds(disappearDelay);

        yield return StartCoroutine(ShrinkNumber());

        Lean.Pool.LeanPool.Despawn(gameObject);
    }

    private IEnumerator ShakeCharacters()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (textInfo.characterInfo[i].isVisible)
                {
                    TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                    int materialIndex = charInfo.materialReferenceIndex;
                    int vertexIndex = charInfo.vertexIndex;

                    Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                    // Constrain the shake to up-down and left-right directions
                    Vector3 offset = new Vector3(
                        Random.Range(-shakeAmount/2, shakeAmount/2),
                        Random.Range(-shakeAmount, shakeAmount),
                        0f
                    );

                    for (int j = 0; j < 4; j++)
                    {
                        vertices[vertexIndex + j] = originalVertices[materialIndex][vertexIndex + j] + offset;
                    }
                }
            }

            // Update the mesh with the new vertex positions
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textMesh.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(0.06f);
        }

        // Reset vertices to original positions
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            System.Array.Copy(originalVertices[i], textInfo.meshInfo[i].vertices, textInfo.meshInfo[i].vertices.Length);
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMesh.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
    private IEnumerator GrowNumber()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < growDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / growDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private IEnumerator ShrinkNumber()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
    
}
