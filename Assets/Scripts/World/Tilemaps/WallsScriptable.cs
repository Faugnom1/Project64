using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class WallsScriptable : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMesh;
    [SerializeField] private ParticleSystem _tileDestroyParticles;

    private Tilemap _tilemap;
    private ShadowCaster2DCreator _shadowCreator;

    // Assuming they all have equal sorting layers
    private int[] _sortingLayerCache;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _shadowCreator = GetComponent<ShadowCaster2DCreator>();
        InitializeSortingLayers();
    }

    public void DestroyTiles(Vector3Int[] tiles, bool playParticles)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            _tilemap.SetTile(tiles[i], null);
            if (playParticles)
            {
                CreateParticlesAtPosition(tiles[i]);
            }
        }
        StartCoroutine(RebakeNavMesh());
        StartCoroutine(RecreateShadows());
    }

    private void CreateParticlesAtPosition(Vector3Int tile)
    {
        Vector2 centerWorld = _tilemap.GetCellCenterWorld(tile);
        Instantiate(_tileDestroyParticles, centerWorld, Quaternion.identity);
    }

    private IEnumerator RebakeNavMesh()
    {
        yield return new WaitForEndOfFrame();
        _navMesh.RemoveData();
        _navMesh.BuildNavMesh();
    }

    private IEnumerator RecreateShadows()
    {
        yield return new WaitForEndOfFrame();
        _shadowCreator.Create();
        SetSortingLayers();
    }

    private void InitializeSortingLayers()
    {
        ShadowCaster2D[] shadowCasters = GetComponentsInChildren<ShadowCaster2D>();
        if (shadowCasters.Length > 0)
        {
            _sortingLayerCache = GetLayers(shadowCasters[0]);
        }
    }

    private void SetSortingLayers()
    {
        ShadowCaster2D[] shadowCasters = GetComponentsInChildren<ShadowCaster2D>();
        for (int i = 0; i < shadowCasters.Length; i++)
        {
            SetLayers(shadowCasters[i], _sortingLayerCache);
        }
    }

    private int[] GetLayers(ShadowCaster2D shadowCaster)
    {
        FieldInfo targetSortingLayersField = typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers",
                                                                   BindingFlags.NonPublic |
                                                                   BindingFlags.Instance);
        int[] mask = targetSortingLayersField.GetValue(shadowCaster) as int[];
        return mask;
    }

    private void SetLayers(ShadowCaster2D shadowCaster, int[] mask)
    {
        FieldInfo targetSortingLayersField = typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers",
                                                                   BindingFlags.NonPublic |
                                                                   BindingFlags.Instance);
        targetSortingLayersField.SetValue(shadowCaster, mask);
    }
}
