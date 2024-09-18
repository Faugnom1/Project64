using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallsScriptable : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMesh;
    [SerializeField] private ParticleSystem _tileDestroyParticles;

    private Tilemap _tilemap;
    private ShadowCaster2DCreator _shadowCreator;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _shadowCreator = GetComponent<ShadowCaster2DCreator>();
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
    }
}
