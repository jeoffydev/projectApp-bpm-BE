using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace asp_bpm_core7_BE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NullApiController : ControllerBase
{
    public bool Null()
    {
        return true;
    }
}