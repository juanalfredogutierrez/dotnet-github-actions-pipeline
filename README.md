# 📌 SUNAT API Integration with Azure Functions

Proyecto que implementa el consumo de la **API pública de SUNAT** utilizando **Azure Functions**, aplicando buenas prácticas de desarrollo como **pruebas unitarias, pruebas de integración y automatización de CI/CD con GitHub Actions**.

---

# 🚀 Tecnologías utilizadas

- .NET
- Azure Functions
- GitHub Actions
- xUnit / NUnit
- HTTP Client
- Microsoft Azure

---

# 🏗️ Arquitectura del proyecto

El proyecto sigue una estructura modular para facilitar la mantenibilidad y escalabilidad.

### Componentes

**Azure Function**

Punto de entrada para consumir la API.

**Application Layer**

Contiene la lógica de negocio.

**Infrastructure**

Cliente HTTP encargado de consumir la API de SUNAT.

**Tests**

Incluye pruebas unitarias y pruebas de integración.

---

# 🔗 Integración con SUNAT

La aplicación consume la **API pública de SUNAT** para obtener información tributaria.

Flujo de la aplicación:

---

# 🧪 Testing

El proyecto incluye:

- Pruebas unitarias
- Pruebas de integración

Ejecutar pruebas:

```bash
dotnet test

.github/workflows/ci-cd.yml


+------------------+
| GitHub Repository |
+------------------+
|
v
+------------------+
| GitHub Actions CI |
+------------------+
|
v
+------------------+
| Azure Functions |
+------------------+
|
v
+-----------+
| API SUNAT |
+-----------+
