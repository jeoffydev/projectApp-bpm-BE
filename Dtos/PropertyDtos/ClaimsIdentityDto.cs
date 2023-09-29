namespace asp_bpm_core7_BE.Dtos.PropertyDtos;

public class ClaimsIdentityDto<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
}