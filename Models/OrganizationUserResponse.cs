namespace asp_bpm_core7_BE.Models;

public class OrganizationUserResponse<T>
{
    public T? Data { get; set; }
    public string ErrorMsg { get; set; } = String.Empty;
    public bool Success { get; set; } = true;
}