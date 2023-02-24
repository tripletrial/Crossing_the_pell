using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dissolver_Stage : MonoBehaviour
{
    [System.Serializable]
    public class RendererMaterials
    {
        public RendererMaterials(Renderer renderer, List<Material> materials)
        {
            this.renderer = renderer;
            this.materials = materials;
        }

        public Renderer renderer;
        public List<Material> materials;
        public List<Material> materialsToReplace;

        public bool shouldReplaceMaterials = false;
    }

    public enum MeshesDetection
    {
        GetComponents,GetComponentsInChildren,GetComponentsInParents
    }

    public List<RendererMaterials> renderers;
    private List<Renderer> meshRenderers;

    public float Duration = 2.7f;
    public bool ShouldMaterializeFirst = false;
    public AnimationCurve DissolveCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public MeshesDetection meshesDetection;

    public bool show;
    private bool showed;
    private bool m_Materialized;
    private bool m_Dissolved;
    private float m_DissolveAmount;
    private bool m_Finished = true;
    public Material targetMaterial;
    private Material[] childrenMaterials;
    private Material[] tempMaterials;

    //Queue Coroutines

    public Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
    

    void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        //Switching the materials to the target material
        meshRenderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
        tempMaterials = new Material[meshRenderers.Count];
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            tempMaterials[i] = meshRenderers[i].material;
        }
        childrenMaterials = new Material[meshRenderers.Count];
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].material = targetMaterial;
        }
        // end of switching materials


        if(ShouldMaterializeFirst)
        {
            m_Materialized = false;
            m_Dissolved = true;
        }
        else
        {
            m_Materialized = true;
            m_Dissolved = false;
        }
        
    }
    void Update()
    {
        
        
        if (show & !showed)
        {
            for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].enabled = true;
        }
        
            ReplaceMaterials();
             MaterializeDissolve();
             StartCoroutine(ReturnMaterials());
             showed = true;
        
        } else if(!show & showed){
            ReplaceMaterials();
            MaterializeDissolve();
            StartCoroutine(ReturnMaterials());
            StartCoroutine(turnOffAllRenderers());
            showed = false;
        }
        
    }

    IEnumerator turnOffAllRenderers()
    {
        yield return new WaitForSeconds(Duration);
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].enabled = false;
        }
        
    }

    IEnumerator ReturnMaterials(){
        yield return new WaitForSeconds(Duration);
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].material = tempMaterials[i];
        }
    }
    /// <summary>
    /// Finds all renderers that would be affected by the dissolver. 
    /// </summary>
    public void FindRenderers()
    {
        switch (meshesDetection)
        {
            case MeshesDetection.GetComponents:
                meshRenderers = new List<Renderer>(GetComponents<Renderer>());
                break;
            case MeshesDetection.GetComponentsInChildren:
                meshRenderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
                break;
            case MeshesDetection.GetComponentsInParents:
                meshRenderers = new List<Renderer>(GetComponentsInParent<Renderer>());
                break;
        }

        renderers = new List<RendererMaterials>();
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            renderers.Add(new RendererMaterials(meshRenderers[i],new List<Material>(meshRenderers[i].sharedMaterials)));

        }

    }

    /// <summary>
    /// Replaces materials in renderers with shouldReplace varaible set to truth with new materials.
    /// </summary>
    public void ReplaceMaterials()
    {
        //Switching the materials to the target material
        meshRenderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
        //tempMaterials = new Material[meshRenderers.Count];

        childrenMaterials = new Material[meshRenderers.Count];
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].material = targetMaterial;
        }
        // end of switching materials
    }

    /// <summary>
    /// Operation is executed if other operations were finished. Function automatically detects which operation to choose between materialize and dissolve.
    /// </summary>
    ///<returns>
    ///True if materialize or dissolve can be performed, otherwise false when the previous action has not ended.
    ///</returns>
    public bool MaterializeDissolve()
    {
        if (!m_Finished) return false;
        m_Finished = false;
        Debug.Log("MaterializeDissolveing");
        if (m_Dissolved)
            StartCoroutine(Materialize(Duration));
        else if (m_Materialized)
            StartCoroutine(Dissolve(Duration));

        return true;
    }
    /// <summary>
    /// When called, operation is added to queue. Function automatically detects which operation to choose between materialize and dissolve.
    /// </summary>
    public void QueueMaterializeDissolve()
    {
        coroutineQueue.Enqueue(QueueMaterializeDissolve(Duration));
    }


    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (coroutineQueue.Count > 0)
                yield return StartCoroutine(coroutineQueue.Dequeue());
            yield return null;
        }
    }

    private IEnumerator QueueMaterializeDissolve(float fadeDuration)
    {
        if(meshRenderers == null)
        {
            FindRenderers();
        }

        float elapsedTime = 0f;

        if(m_Dissolved)
        {

            m_Materialized = true;
            m_Dissolved = false;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                m_DissolveAmount = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

                foreach (var item in meshRenderers)
                {
                    foreach (var mat in item.materials)
                    {
                        mat.SetFloat("_DissolveAmount", m_DissolveAmount);
                    }
                }

                yield return null;
            }

            m_Finished = true;
        }

        else if(m_Materialized)
        {


            m_Materialized = false;
            m_Dissolved = true;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                m_DissolveAmount = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                foreach (var list in meshRenderers)
                {
                    foreach (var mat in list.materials)
                    {
                        mat.SetFloat("_DissolveAmount", m_DissolveAmount);
                    }
                }
                yield return null;
            }


            m_Finished = true;
        }

    }

    private IEnumerator Materialize(float fadeDuration)
    {
        if (meshRenderers == null)
        {
            FindRenderers();
        }

        float elapsedTime = 0f;

        m_Materialized = true;
        m_Dissolved = false;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            m_DissolveAmount = Mathf.Lerp(1, 0,elapsedTime / fadeDuration);

            foreach (var list in meshRenderers)
            {
                foreach (var mat in list.materials)
                {
                    mat.SetFloat("_DissolveAmount", m_DissolveAmount);
                }
            }
            
            yield return null;
        }
        
        Debug.Log("Materialized+Replaced");
        m_Finished = true;
    }

    private IEnumerator Dissolve(float fadeDuration)
    {
        if (meshRenderers == null)
        {
            FindRenderers();
        }

        float elapsedTime = 0f;

        m_Materialized = false;
        m_Dissolved = true;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            m_DissolveAmount = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            foreach (var list in meshRenderers)
            {
                foreach (var mat in list.materials)
                {
                    mat.SetFloat("_DissolveAmount", m_DissolveAmount);
                }
            }
            yield return null;
        }


        m_Finished = true;
    }
}
