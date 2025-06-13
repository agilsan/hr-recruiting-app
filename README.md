# Recruiting App – Gestión de Candidatos Junior

Bienvenido al proyecto **Recruiting App**, una solución full‑stack pensada para agilizar la recepción de CV, la planificación de entrevistas y la priorización de candidatos dentro del departamento de Desarrollo.

---

## Tabla de contenido

1. [Características principales](#características-principales)
2. [Stack tecnológico](#stack-tecnológico)
3. [Requisitos previos](#requisitos-previos)
4. [Puesta en marcha rápida](#puesta-en-marcha-rápida)
5. [Estructura de contenedores](#estructura-de-contenedores)
6. [Arquitectura del proyecto](#arquitectura-del-proyecto)
7. [Credenciales de ejemplo](#credenciales-de-ejemplo)
8. [Flujo de trabajo típico](#flujo-de-trabajo-típico)
9. [Variables de entorno](#variables-de-entorno)
10. [Métricas y dashboard](#métricas-y-dashboard)
11. [Pruebas](#pruebas)
12. [Próximos pasos / TODO](#próximos-pasos--todo)

---

## Características principales

* **Carga de CV (PDF)** desde e‑mail o Teams, almacenados localmente.
* **Extracción automática** de nombre y foto (primera imagen del PDF) mediante iText 7.
* **Estados del candidato**: Nuevo, EntrevistaProgramada, Entrevistado, Finalista, Acepta, RechazadoEmpresa, RechazadoCandidato, Silver.
* **Planificación manual de entrevistas** (zona horaria España).
* **Notas** en texto plano enlazadas a cada entrevista.
* **Ranking diario** con drag & drop y numeración automática.
* **Motivos de descarte** (lista predeterminada + campo Otros).
* **Recordatorio de candidatos Silver** al iniciar un nuevo proceso.
* **Dashboard** con métricas clave (tiempo medio de contratación, fuentes de CV, ratios de éxito y descarte).

---

## Stack tecnológico

| Capa         | Tecnología                                                             |
| ------------ | ---------------------------------------------------------------------- |
| Frontend     | Vue 3 + Vite + TypeScript + Pinia + Tailwind + vuedraggable + Chart.js |
| Backend      | .NET 8 Web API + Entity Framework Core                                 |
| Base datos   | PostgreSQL 16                                                          |
| Auth         | ASP.NET Identity + JWT                                                 |
| Contenedores | Docker & docker‑compose                                                |

---

## Requisitos previos

* **Docker Desktop 4.x** o compatible (Linux/Mac/Windows).
* Docker Compose v2 (incluido en Docker Desktop > 4.x).

---

## Puesta en marcha rápida

```bash
# 1. Clonar el repositorio
$ git clone https://github.com/tu‑usuario/hr‑recruiting‑app.git
$ cd hr‑recruiting‑app

# 2. Construir y levantar contenedores
$ docker compose up -d --build

# 3. Abrir la aplicación
Frontend:  http://localhost:8080
Swagger:   http://localhost:5000/swagger
DB admin (opcional): conectar a localhost:5432, db=hrapp, user=hrapp, pass=hrapp
```

Para detener los servicios:

```bash
docker compose down
```

---

## Estructura de contenedores

```text
docker-compose.yml
├─ db       (postgres:16)        → 5432
├─ api      (.NET 8 API)         → 5000
└─ web      (Vue 3 static site)  → 8080
```

> Los PDF se guardan en el volumen **cv-storage** montado en `/app/cv-storage` dentro del contenedor **api**.

---

## Arquitectura del proyecto

```
hr‑recruiting‑app/
├─ api/          # Código fuente .NET 8 (Web API)
│  ├─ Controllers/
│  ├─ Services/
│  ├─ Models/
│  └─ Program.cs
├─ web/          # Frontend Vue 3 (Vite)
│  ├─ src/
│  │  ├─ components/
│  │  ├─ pages/
│  │  └─ stores/
│  └─ vite.config.ts
└─ docker-compose.yml
```

### Backend

* **Program.cs** configura Swagger, autenticación JWT y EF Core.
* **Servicios**: `CVService`, `InterviewService`, `RankingService`, `MetricsService`.
* **Modelo de datos** en `Models/` y migraciones automáticas al arrancar.

### Frontend

* **Vue Router** para navegación.
* **Pinia** para estado global.
* **Tailwind** como sistema de diseño.
* **vuedraggable** para el ranking diario.
* **Chart.js** envoltura para métricas.

---

## Credenciales de ejemplo

El seeding inicial crea un usuario de prueba:

| Rol   | Usuario                                     | Contraseña |
| ----- | ------------------------------------------- | ---------- |
| Admin | [admin@acme.local](mailto:admin@acme.local) | P\@ssw0rd! |

> Cambia la contraseña en tu primer inicio de sesión.

---

## Flujo de trabajo típico

1. **Subir CV** en *Candidates → Upload*.  Se extraen datos y se crea el candidato.
2. **Programar entrevista** desde *Interviews*.
3. **Tomar notas** durante la entrevista.
4. **Ordenar candidatos** cada día en *Rankings*.  Drag & drop actualiza la posición numérica.
5. **Descartar o aceptar** cambiando estado y motivo.
6. **Revisar métricas** en *Dashboard*.
7. Al iniciar un nuevo proceso, la app muestra los candidatos `Silver` para contactar de nuevo.

---

## Variables de entorno

Las principales están definidas en `docker-compose.yml`, pero puedes sobreescribirlas con un archivo `.env`:

```env
POSTGRES_PASSWORD=otraPass
JWT_SECRET=SúperSecreto123
```

---

## Métricas y dashboard

| Métrica                      | Descripción                                      |
| ---------------------------- | ------------------------------------------------ |
| Tiempo medio de contratación | Días entre primera entrevista y estado `Acepta`. |
| Fuentes de CV                | Email vs Teams.                                  |
| Ratio entrevistados vs éxito | Proporción aceptación / descarte.                |

---

## Pruebas

* **Backend**: Ejecuta `dotnet test` dentro del contenedor `api` para lanzar las pruebas xUnit.
* **Frontend**: (opcional) Jest + Vue Test Utils.

---

## Próximos pasos / TODO

* Integración API Confluence para crear páginas de notas automáticamente.
* Histórico de cambios en ranking.
* CI/CD GitHub Actions.
* Soporte para extracción automática del CV directamente desde Outlook o Teams webhook.
