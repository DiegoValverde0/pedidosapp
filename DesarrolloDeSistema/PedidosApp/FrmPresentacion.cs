using System;
using System.Windows.Forms;
using CapaNegocio;

namespace PedidosApp
{
    public partial class FrmPresentacion : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;
        private static FrmPresentacion _Instancia;

        public static FrmPresentacion GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new FrmPresentacion();
            }
            return _Instancia;
        }

        public FrmPresentacion()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre de la categoría");
            this.ttMensaje.SetToolTip(this.txtDescripcion, "Ingrese una descripción de la categoría");
            this.LlenarGrid();
        }

        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
        }

        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.btnGuardar.Enabled = valor;
            this.btnCancelar.Enabled = valor;
        }

        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        private void LlenarGrid()
        {
            this.dataListado.DataSource = NPresentacion.Mostrar();
            this.lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void BuscarNombre()
        {
            this.dataListado.DataSource = NPresentacion.BuscarNombre(this.txtBuscar.Text);
            this.lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void FrmPresentacion_Load(object sender, EventArgs e)
        {
            this.LlenarGrid();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un valor");
                }
                else
                {
                    // Validar si se está insertando o editando
                    if (this.IsNuevo)
                    {
                        rpta = NPresentacion.Insertar(txtNombre.Text.Trim().ToUpper(), txtDescripcion.Text.Trim());
                    }
                    else
                    {
                        // Obtenemos el id de la fila seleccionada para editar
                        int idPresentacion = Convert.ToInt32(dataListado.CurrentRow.Cells["idpresentacion"].Value);
                        rpta = NPresentacion.Editar(idPresentacion, txtNombre.Text.Trim().ToUpper(), txtDescripcion.Text.Trim());
                    }

                    // Mensaje de resultado
                    if (rpta.Equals("OK"))
                    {
                        MensajeOK(this.IsNuevo ? "Se guardó el registro" : "Se actualizó el registro");
                    }
                    else
                    {
                        MensajeError(rpta);
                    }

                    IsNuevo = false;
                    IsEditar = false;
                    Botones();
                    Limpiar();
                    LlenarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
            {
                IsEditar = true;
                Botones();
                Habilitar(true);

                // Cargar datos seleccionados en los campos de texto
                txtNombre.Text = Convert.ToString(dataListado.CurrentRow.Cells["nombre"].Value);
                txtDescripcion.Text = Convert.ToString(dataListado.CurrentRow.Cells["descripcion"].Value);
            }
            else
            {
                MensajeError("Debe seleccionar primero el registro para modificar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            IsNuevo = false;
            IsEditar = false;
            Botones();
            Limpiar();
        }

        private void dataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = Convert.ToString(dataListado.CurrentRow.Cells["nombre"].Value);
            txtDescripcion.Text = Convert.ToString(dataListado.CurrentRow.Cells["descripcion"].Value);
            IsEditar = true;
            Botones();
            Habilitar(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Está seguro de borrar los registros?", "Pedidos App", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string rpta = "";
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            int Codigo = Convert.ToInt32(row.Cells["idpresentacion"].Value);
                            rpta = NPresentacion.Eliminar(Codigo);
                            if (rpta.Equals("OK"))
                            {
                                MensajeOK("Se borraron los registros");
                            }
                            else
                            {
                                MensajeError(rpta);
                            }
                        }
                    }
                    LlenarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void FrmPresentacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }
    }
}
