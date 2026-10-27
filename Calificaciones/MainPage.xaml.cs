namespace Calificaciones;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNotaChanged(object? sender, TextChangedEventArgs e)
    {
        double.TryParse(txtSeg1?.Text, out double seg1);
        double.TryParse(txtEx1?.Text, out double ex1);
        double.TryParse(txtSeg2?.Text, out double seg2);
        double.TryParse(txtEx2?.Text, out double ex2);

        double p1 = (seg1 * 0.3) + (ex1 * 0.2);
        double p2 = (seg2 * 0.3) + (ex2 * 0.2);
        double final = p1 + p2;

        if (lblResumenNota != null)
        {
            lblResumenNota.Text = $"Nota Final Actual: {final:F2}";
        }

        if (lblResumenEstado != null)
        {
            if (final >= 7.0)
            {
                lblResumenEstado.Text = "🟢 Estado: Aprobado";
                lblResumenEstado.TextColor = Color.FromArgb("#3FB950");
            }
            else if (final >= 5.0 && final <= 6.9)
            {
                lblResumenEstado.Text = "🟡 Estado: Complementario";
                lblResumenEstado.TextColor = Color.FromArgb("#D29922");
            }
            else
            {
                lblResumenEstado.Text = "🔴 Estado: Reprobado";
                lblResumenEstado.TextColor = Color.FromArgb("#F85149");
            }
        }
    }

    private async void OnCalcularClicked(object? sender, EventArgs e)
    {
        if (pickerEstudiante.SelectedIndex == -1 || pickerEstudiante.SelectedItem == null)
        {
            await DisplayAlertAsync("Error de Validación", "Por favor seleccione un estudiante de la lista desplegable.", "Aceptar");
            return;
        }

        string nombreEstudiante = pickerEstudiante.SelectedItem.ToString() ?? "Sin nombre";

        DateTime fecha = datePickerFecha.Date ?? DateTime.Now;
        string fechaSeleccionada = $"{fecha.Day:D2}/{fecha.Month:D2}/{fecha.Year}";

        if (!double.TryParse(txtSeg1.Text, out double seg1) ||
            !double.TryParse(txtEx1.Text, out double ex1) ||
            !double.TryParse(txtSeg2.Text, out double seg2) ||
            !double.TryParse(txtEx2.Text, out double ex2))
        {
            await DisplayAlertAsync("Error de Entrada", "Todos los campos de notas deben contener valores numéricos válidos.", "Aceptar");
            return;
        }

        if (seg1 < 0 || seg1 > 10 || ex1 < 0 || ex1 > 10 ||
            seg2 < 0 || seg2 > 10 || ex2 < 0 || ex2 > 10)
        {
            await DisplayAlertAsync("Rango Inválido", "Las notas de seguimiento y examen deben estar estrictamente entre 0 y 10.", "Aceptar");
            return;
        }

        double notaParcial1 = (seg1 * 0.3) + (ex1 * 0.2);
        double notaParcial2 = (seg2 * 0.3) + (ex2 * 0.2);
        double notaFinal = notaParcial1 + notaParcial2;

        string estado = "";
        if (notaFinal >= 7.0)
        {
            estado = "Aprobado";
        }
        else if (notaFinal >= 5.0 && notaFinal <= 6.9)
        {
            estado = "Complementario";
        }
        else
        {
            estado = "REPROBADO";
        }

        string mensajeAlert = $"Nombre: {nombreEstudiante}\n" +
                              $"Fecha: {fechaSeleccionada}\n" +
                              $"Nota Parcial 1: {notaParcial1:F2}\n" +
                              $"Nota Parcial 2: {notaParcial2:F2}\n" +
                              $"Nota Final: {notaFinal:F2}\n" +
                              $"Estado: {estado}";

        await DisplayAlertAsync("🏆 Reporte Oficial del Estudiante", mensajeAlert, "Aceptar");
    }
}