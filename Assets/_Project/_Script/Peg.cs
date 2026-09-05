using UnityEngine;

public class Peg : MonoBehaviour
{
    [SerializeField] private PegType _type = PegType.Blue;
    [SerializeField] private int _baseScore = 10;

    [Header("Visual")]
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _hitIntensity = 2f;

    private bool _hit;
    private MaterialPropertyBlock _propertyBlock;
    private Color _baseColor;

    private static readonly int ColorID = Shader.PropertyToID("_Color");

    public bool IsHit => _hit;
    public PegType Type => _type;

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _baseColor = _renderer.sharedMaterial.GetColor(ColorID);
    }

    private void OnEnable()
    {
        ShotEvents.ShotEnded += OnShotEnded;
    }

    private void OnDisable()
    {
        ShotEvents.ShotEnded -= OnShotEnded;
    }

    private void OnShotEnded(bool freeBall)
    {
        if (!_hit) return;

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si vuelve a pegar no se repite la funcion.
        if (_hit) return;

        _hit = true;

        SetColorIntensity(_hitIntensity);
        PegEvents.RaiseHit(new PegHitData(_type, _baseScore));
    }

    private void SetColorIntensity(float intensity)
    {
        _renderer.GetPropertyBlock(_propertyBlock);

        Color hdrColor = _baseColor * Mathf.Pow(2f, intensity);

        _propertyBlock.SetColor(ColorID, hdrColor);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
