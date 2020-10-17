namespace Net.Data
{
    public interface IRepositoryWrapper
    {
        ICalidadRepository Calidad { get; }
        ICondicionLimpiezaRepository CondicionLimpieza { get; }
        IEmpresaRepository Empresa { get; }
        IEquipoRepository Equipo { get; }
        IMantenimientoPorModeloRepository MantenimientoPorModelo { get; }
        IMantenimientoRepository Mantenimiento { get; }
        IModeloRepository Modelo { get; }
        IPlantaRepository Planta { get; }
        IProcesoRepository Proceso { get; }
        IProcesoDetalleRepository ProcesoDetalle { get; }
        IRepuestoPorModeloRepository RepuestoPorModelo { get; }
        IEquipoPorModeloRepository EquipoPorModelo { get; }
        IRequerimientoEquipoRepository RequerimientoEquipo { get; }
        ITxRegistroEquipoRepository TxRegistroEquipo { get; }
        ITxRegistroEquipoDetalle1Repository TxRegistroEquipoDetalle1 { get; }
    }
}
