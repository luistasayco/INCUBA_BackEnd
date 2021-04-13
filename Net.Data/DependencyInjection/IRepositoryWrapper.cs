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
        ITxExamenFisicoPollitoRepository TxExamenFisicoPollito { get; }
        ITipoExplotacionRepository TipoExplotacion { get; }
        ISubTipoExplotacionRepository SubTipoExplotacion { get; }
        ITxRegistroDocumentoRepository TxRegistroDocumento { get; }
        IEmailSenderRepository EmailSender { get; }
        IBoquillaRepository Boquilla { get; }
        IProcesoSprayRepository ProcesoSpray { get; }
        IProcesoDetalleSprayRepository ProcesoDetalleSpray { get; }
        ITxVacunacionSprayRepository TxVacunacionSpray { get; }
        IAgujaRepository Aguja { get; }
        IIrregularidadRepository Irregularidad { get; }
        IProcesoSubCutaneaRepository ProcesoSubCutanea { get; }
        IProcesoDetalleSubCutaneaRepository ProcesoDetalleSubCutanea { get; }
        ITxVacunacionSubCutaneaRepository TxVacunacionSubCutanea { get; }
        IVacunaRepository Vacuna { get; }
        IIndiceEficienciaRepository IndiceEficiencia { get; }
        ITxSIMRepository TxSIM { get; }
        ITxSIMConsolidadoRepository TxSIMConsolidado { get; }
        IOrganoRepository Organo { get; }
        IOrganoDetalleRepository OrganoDetalle { get; }
        ITxSINMIRepository TxSINMI { get; }
        ITxSINMIConsolidadoRepository TxSINMIConsolidado { get; }
    }
}
