namespace asp_bpm_core7_BE.Dtos;

public class VerifySecretKeyDto
{
    public string Email { get; set; } = String.Empty;
    public bool Verified { get; set; } = false;
}