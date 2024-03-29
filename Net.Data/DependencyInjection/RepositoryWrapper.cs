﻿using Net.Connection;

namespace Net.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IConnectionSQL _repoContext;

        private IDashboardFormularioRepository _DashboardFormulario;
        private IDashboardAuditoriaRepository _DashboardAuditoria;
        private IDashboardSINMIRepository _DashboardSINMI;
        private IDashboardRepository _Dashboard;
        private IDashboardMantenimientoRepository _DashboardMantenimiento;
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
        private IBoquillaRepository _Boquilla;
        private IProcesoSprayRepository _ProcesoSpray;
        private IProcesoDetalleSprayRepository _ProcesoDetalleSpray;
        private ITxVacunacionSprayRepository _TxVacunacionSpray;
        private IAgujaRepository _Aguja;
        private IIrregularidadRepository _Irregularidad;
        private IProcesoSubCutaneaRepository _ProcesoSubCutanea;
        private IProcesoDetalleSubCutaneaRepository _ProcesoDetalleSubCutanea;
        private ITxVacunacionSubCutaneaRepository _TxVacunacionSubCutanea;
        private IVacunaRepository _Vacuna;
        private IIndiceEficienciaRepository _IndiceEficiencia;
        private ITxSIMRepository _TxSIM;
        private ITxSIMConsolidadoRepository _TxSIMConsolidado;
        private IOrganoRepository _Organo;
        private IOrganoDetalleRepository _OrganoDetalle;
        private ITxSINMIRepository _TxSINMI;
        private ITxSINMIConsolidadoRepository _TxSINMIConsolidado;

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
        public IBoquillaRepository Boquilla
        {
            get
            {
                if (_Boquilla == null)
                {
                    _Boquilla = new BoquillaRepository(_repoContext);
                }
                return _Boquilla;
            }
        }
        public IProcesoSprayRepository ProcesoSpray
        {
            get
            {
                if (_ProcesoSpray == null)
                {
                    _ProcesoSpray = new ProcesoSprayRepository(_repoContext);
                }
                return _ProcesoSpray;
            }
        }
        public IProcesoDetalleSprayRepository ProcesoDetalleSpray
        {
            get
            {
                if (_ProcesoDetalleSpray == null)
                {
                    _ProcesoDetalleSpray = new ProcesoDetalleSprayRepository(_repoContext);
                }
                return _ProcesoDetalleSpray;
            }
        }
        public ITxVacunacionSprayRepository TxVacunacionSpray
        {
            get
            {
                if (_TxVacunacionSpray == null)
                {
                    _TxVacunacionSpray = new TxVacunacionSprayRepository(_repoContext);
                }
                return _TxVacunacionSpray;
            }
        }
        public IAgujaRepository Aguja
        {
            get
            {
                if (_Aguja == null)
                {
                    _Aguja = new AgujaRepository(_repoContext);
                }
                return _Aguja;
            }
        }
        public IIrregularidadRepository Irregularidad
        {
            get
            {
                if (_Irregularidad == null)
                {
                    _Irregularidad = new IrregularidadRepository(_repoContext);
                }
                return _Irregularidad;
            }
        }
        public IProcesoSubCutaneaRepository ProcesoSubCutanea
        {
            get
            {
                if (_ProcesoSubCutanea == null)
                {
                    _ProcesoSubCutanea = new ProcesoSubCutaneaRepository(_repoContext);
                }
                return _ProcesoSubCutanea;
            }
        }
        public IProcesoDetalleSubCutaneaRepository ProcesoDetalleSubCutanea
        {
            get
            {
                if (_ProcesoDetalleSubCutanea == null)
                {
                    _ProcesoDetalleSubCutanea = new ProcesoDetalleSubCutaneaRepository(_repoContext);
                }
                return _ProcesoDetalleSubCutanea;
            }
        }
        public ITxVacunacionSubCutaneaRepository TxVacunacionSubCutanea
        {
            get
            {
                if (_TxVacunacionSubCutanea == null)
                {
                    _TxVacunacionSubCutanea = new TxVacunacionSubCutaneaRepository(_repoContext);
                }
                return _TxVacunacionSubCutanea;
            }
        }
        public IVacunaRepository Vacuna
        {
            get
            {
                if (_Vacuna == null)
                {
                    _Vacuna = new VacunaRepository(_repoContext);
                }
                return _Vacuna;
            }
        }
        public IIndiceEficienciaRepository IndiceEficiencia
        {
            get
            {
                if (_IndiceEficiencia == null)
                {
                    _IndiceEficiencia = new IndiceEficienciaRepository(_repoContext);
                }
                return _IndiceEficiencia;
            }
        }
        public ITxSIMRepository TxSIM
        {
            get
            {
                if (_TxSIM == null)
                {
                    _TxSIM = new TxSIMRepository(_repoContext);
                }
                return _TxSIM;
            }
        }
        public ITxSIMConsolidadoRepository TxSIMConsolidado
        {
            get
            {
                if (_TxSIMConsolidado == null)
                {
                    _TxSIMConsolidado = new TxSIMConsolidadoRepository(_repoContext);
                }
                return _TxSIMConsolidado;
            }
        }
        public IOrganoRepository Organo
        {
            get
            {
                if (_Organo == null)
                {
                    _Organo = new OrganoRepository(_repoContext);
                }
                return _Organo;
            }
        }
        public IOrganoDetalleRepository OrganoDetalle
        {
            get
            {
                if (_OrganoDetalle == null)
                {
                    _OrganoDetalle = new OrganoDetalleRepository(_repoContext);
                }
                return _OrganoDetalle;
            }
        }
        public ITxSINMIRepository TxSINMI
        {
            get
            {
                if (_TxSINMI == null)
                {
                    _TxSINMI = new TxSINMIRepository(_repoContext);
                }
                return _TxSINMI;
            }
        }

        public ITxSINMIConsolidadoRepository TxSINMIConsolidado
        {
            get
            {
                if (_TxSINMIConsolidado == null)
                {
                    _TxSINMIConsolidado = new TxSINMIConsolidadoRepository(_repoContext);
                }
                return _TxSINMIConsolidado;
            }
        }

        public IDashboardMantenimientoRepository DashboardMantenimiento
        {
            get 
            {
                if (_DashboardMantenimiento == null)
                {
                    _DashboardMantenimiento = new DashboardMantenimientoRepository(_repoContext);
                }
                return _DashboardMantenimiento;
            }
        }

        public IDashboardRepository Dashboard
        {
            get
            {
                if (_Dashboard == null)
                {
                    _Dashboard = new DashboardRepository(_repoContext);
                }
                return _Dashboard;
            }
        }

        public IDashboardAuditoriaRepository DashboardAuditoria
        {
            get
            {
                if (_DashboardAuditoria == null)
                {
                    _DashboardAuditoria = new DashboardAuditoriaRepository(_repoContext);
                }
                return _DashboardAuditoria;
            }
        }

        public IDashboardFormularioRepository DashboardFormulario
        {
            get
            {
                if (_DashboardFormulario == null)
                {
                    _DashboardFormulario = new DashboardFormularioRepository(_repoContext);
                }
                return _DashboardFormulario;
            }
        }

        public IDashboardSINMIRepository DashboardSINMI
        {
            get
            {
                if (_DashboardSINMI == null)
                {
                    _DashboardSINMI = new DashboardSINMIRepository(_repoContext);
                }
                return _DashboardSINMI;
            }
        }
    }
}
