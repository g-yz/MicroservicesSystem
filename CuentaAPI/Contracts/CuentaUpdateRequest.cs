﻿namespace CuentaAPI.Contracts;

public class CuentaUpdateRequest
{
    public required string NumeroCuenta { get; set; }

    public decimal SaldoInicial { get; set; }

    public int TipoCuentaId { get; set; }

    public bool Estado { get; set; }

    public Guid ClienteId { get; set; }
}