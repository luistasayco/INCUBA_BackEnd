using Net.Connection;

namespace Net.Data
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private IConnectionSQL _repoContext;

        private ICalidadRepository _Calidad;
        private ICondicionLimpiezaRepository _CondicionLimpieza;
        private IEmpresaRepository _Empresa;
        private IEquipoRepository _Equipo;
        private IMantenimientoPorModeloRepository _MantenimientoPorModelo;
        private IMantenimientoRepository _Mantenimiento;
        private IModeloRepository _Modelo;
        private IPlantaRepository _Planta;
        private IProcesoRepository _Proceso;
        private IProcesoDetalleRepository _ProcesoDetalle;
        private IRepuestoPorModeloRepository _RepuestoPorModelo;
        private IEquipoPorModeloRepository _EquipoPorModelo;
        private IRequerimientoEquipoRepository _RequerimientoEquipo;
        private ITxRegistroEquipoRepository _TxRegistroEquipo;
        private ITxRegistroEquipoDetalle1Repository _TxRegistroEquipoDetalle1;
        private ITxExamenFisicoPollitoRepository _TxExamenFisicoPollito;
        private ITipoExplotacionRepository _TipoExplotacion;
        private ISubTipoExplotacionRepository _SubTipoExplotacion;
        private ITxRegistroDocumentoRepository _TxRegistroDocumento;
        private IEmailSenderRepository _EmailSender;

        public RepositoryWrapper(IConnectionSQL repoContext)
        {
            _repoContext = repoContext;
        }

        public ICalidadRepository Calidad
        {
            get
            {
                if (_Calidad == null)
                {
                    _Calidad = new CalidadRepository(_repoContext);
                }
                return _Calidad;
            }
        }
        public ICondicionLimpiezaRepository CondicionLimpieza
        {
            get
            {
                if (_CondicionLimpieza == null)
                {
                    _CondicionLimpieza = new CondicionLimpiezaRepository(_repoContext);
                }
                return _CondicionLimpieza;
            }
        }
        public IEmpresaRepository Empresa
        {
            get
            {
                if (_Empresa == null)
                {
                    _Empresa = new EmpresaRepository(_repoContext);
                }
                return _Empresa;
            }
        }
        public IEquipoRepository Equipo
        {
            get
            {
                if (_Equipo == null)
                {
                    _Equipo = new EquipoRepository(_repoContext);
                }
                return _Equipo;
            }
        }
        public IMantenimientoPorModeloRepository MantenimientoPorModelo
        {
            get
            {
                if (_MantenimientoPorModelo == null)
                {
                    _MantenimientoPorModelo = new MantenimientoPorModeloRepository(_repoContext);
                }
                return _MantenimientoPorModelo;
            }
        }
        public IMantenimientoRepository Mantenimiento
        {
            get
            {
                if (_Mantenimiento == null)
                {
                    _Mantenimiento = new MantenimientoRepository(_repoContext);
                }
                return _Mantenimiento;
            }
        }
        public IModeloRepository Modelo
        {
            get
            {
                if (_Modelo == null)
                {
                    _Modelo = new ModeloRepository(_repoContext);
                }
                return _Modelo;
            }
        }
        public IPlantaRepository Planta
        {
            get
            {
                if (_Planta == null)
                {
                    _Planta = new PlantaRepository(_repoContext);
                }
                return _Planta;
            }
        }
        public IProcesoRepository Proceso
        {
            get
            {
                if (_Proceso == null)
                {
                    _Proceso = new ProcesoRepository(_repoContext);
                }
                return _Proceso;
            }
        }
        public IProcesoDetalleRepository ProcesoDetalle
        {
            get
            {
                if (_ProcesoDetalle == null)
                {
                    _ProcesoDetalle = new ProcesoDetalleRepository(_repoContext);
                }
                return _ProcesoDetalle;
            }
        }
        public IRepuestoPorModeloRepository RepuestoPorModelo
        {
            get
            {
                if (_RepuestoPorModelo == null)
                {
                    _RepuestoPorModelo = new RepuestoPorModeloRepository(_repoContext);
                }
                return _RepuestoPorModelo;
            }
        }
        public IEquipoPorModeloRepository EquipoPorModelo
        {
            get
            {
                if (_EquipoPorModelo == null)
                {
                    _EquipoPorModelo = new EquipoPorModeloRepository(_repoContext);
                }
                return _EquipoPorModelo;
            }
        }
        public IRequerimientoEquipoRepository RequerimientoEquipo
        {
            get
            {
                if (_RequerimientoEquipo == null)
                {
                    _RequerimientoEquipo = new RequerimientoEquipoRepository(_repoContext);
                }
                return _RequerimientoEquipo;
            }
        }
        public ITxRegistroEquipoRepository TxRegistroEquipo
        {
            get
            {
                if (_TxRegistroEquipo == null)
                {
                    _TxRegistroEquipo = new TxRegistroEquipoRepository(_repoContext);
                }
                return _TxRegistroEquipo;
            }
        }
        public ITxRegistroEquipoDetalle1Repository TxRegistroEquipoDetalle1
        {
            get
            {
                if (_TxRegistroEquipoDetalle1 == null)
                {
                    _TxRegistroEquipoDetalle1 = new TxRegistroEquipoDetalle1Repository(_repoContext);
                }
                return _TxRegistroEquipoDetalle1;
            }
        }
        public ITxExamenFisicoPollitoRepository TxExamenFisicoPollito
        {
            get
            {
                if (_TxExamenFisicoPollito == null)
                {
                    _TxExamenFisicoPollito = new TxExamenFisicoPollitoRepository(_repoContext);
                }
                return _TxExamenFisicoPollito;
            }
        }
        public ITipoExplotacionRepository TipoExplotacion
        {
            get
            {
                if (_TipoExplotacion == null)
                {
                    _TipoExplotacion = new TipoExplotacionRepository(_repoContext);
                }
                return _TipoExplotacion;
            }
        }
        public ISubTipoExplotacionRepository SubTipoExplotacion
        {
            get
            {
                if (_SubTipoExplotacion == null)
                {
                    _SubTipoExplotacion = new SubTipoExplotacionRepository(_repoContext);
                }
                return _SubTipoExplotacion;
            }
        }
        public ITxRegistroDocumentoRepository TxRegistroDocumento
        {
            get
            {
                if (_TxRegistroDocumento == null)
                {
                    _TxRegistroDocumento = new TxRegistroDocumentoRepository(_repoContext);
                }
                return _TxRegistroDocumento;
            }
        }

        public IEmailSenderRepository EmailSender
        {
            get
            {
                if (_EmailSender == null)
                {
                    _EmailSender = new EmailSenderRepository(_repoContext);
                }
                return _EmailSender;
            }
        }
    }
}
