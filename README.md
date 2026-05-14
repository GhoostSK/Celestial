<h1 align="center">🌌 Celestial.Agency – Modular .NET Platform (In Development)</h1>

<p align="center">
  <strong>A clean, extensible .NET 10 solution , CQRS, and Clean Architecture principles.</strong><br>
  <em>Part of the Celestial Project Series</em>
</p>

<hr>

<h2>🚀 Overview</h2>
<p>
Celestial.Agency is a modular and layered .NET platform designed for enterprise-grade applications.  
It integrates <b>ASP.NET Core 10</b>, <b>Entity Framework Core</b>, and <b>MediatR</b> to deliver clean separation of concerns and high scalability.
</p>

<p><b>⚙️ Note:</b> This project is currently under active development — new features and modules are being added continuously.</p>

<hr>

<h2>🧩 Project Structure</h2>

<pre>
Celestial.Agency/
│
├── src/
│   ├── Celestial.Agency.Api/         # API Gateway (Presentation Layer)
│   │   ├── Controllers/              # REST endpoints
│   │   ├── Areas/                    # Identity & Admin
│   │   ├── Models/                   # DTOs and ViewModels
│   │   ├── Data/                     # Identity & EF configurations
│   │   ├── appsettings.json          # Configurations
│   │   └── Program.cs                # Entry point
│   │
│   ├── Celestial.Agency.Core/        # Domain Layer
│   │   ├── DAL/                      # Repositories and Interfaces
│   │   │   ├── Model/                # Domain Entities
│   │   │   └── Repositories/         # Repository interfaces
│   │   ├── Enum/                     # Enumerations
│   │   ├── Attributes/               # Custom annotations
│   │   ├── Services/                 # Domain services contracts
│   │   └── AppSetting.cs             # Configuration models
│   │
│   ├── Celestial.Agency.DAL.EF/      # Infrastructure Layer (EF Core)
│   │   ├── Context/                  # DbContext
│   │   ├── Configuration/            # Fluent API setup
│   │   ├── DependencyInjection/      # EF registration
│   │   ├── Seeder/                   # Initial data seeding
│   │   ├── _01-add_migrations.cmd    # Migration helper
│   │   ├── _02-update_db.cmd         # DB update helper
│   │   └── _03-remove_db.cmd         # Cleanup helper
│   │
│   └── Celestial.Agency.Service/     # Application Layer
│       ├── Command/                  # CQRS Commands
│       ├── CommandHandler/           # MediatR Handlers
│       ├── Behavior/                 # Pipeline behaviors
│       ├── Service/                  # App services
│       └── Dependencies/             # DI registrations
│
└── test/                             # Future tests (Unit/Integration)
</pre>

<hr>

<h2>🧱 Architecture</h2>
<ul>
  <li><b>Clean Architecture:</b> Strict separation between layers</li>
  <li><b>CQRS + MediatR:</b> Independent command/query handling</li>
  <li><b>DDD:</b> Domain-driven modeling and encapsulation</li>
  <li><b>EF Core:</b> Persistence layer following repository patterns</li>
  <li><b>Localization:</b> Multi-language entity support via LocalizedProperty</li>
</ul>

<hr>

<h2>🧠 Design Principles</h2>
<ul>
  <li>Command Query Responsibility Segregation (CQRS)</li>
  <li>Repository & Unit of Work Patterns</li>
  <li>Dependency Injection everywhere</li>
  <li>Scalable and modular system</li>
</ul>

<hr>

<h2>🧰 Technologies</h2>

<table>
  <tr><th>Layer</th><th>Technology</th></tr>
  <tr><td>Framework</td><td>.NET 10.0</td></tr>
  <tr><td>ORM</td><td>Entity Framework Core</td></tr>
  <tr><td>Architecture</td><td>Clean Architecture, CQRS</td></tr>
  <tr><td>Communication</td><td>MediatR</td></tr>
  <tr><td>Mapping</td><td>Mapster</td></tr>
  <tr><td>Database</td><td>Microsoft SQL Server</td></tr>
</table>

<hr>

<h2>⚙️ Setup & Run</h2>

<h3>1️⃣ Clone the Repository</h3>
<pre>
git clone https://github.com/GhoostSK/Celestial.Agency.git
cd Celestial.Agency/src/Celestial.Agency.Api
</pre>

<h3>2️⃣ Configure the Database</h3>
<p>Edit your <code>appsettings.json</code>:</p>
<pre>
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CelestialAgency;Trusted_Connection=True;TrustServerCertificate=True;"
}
</pre>

<h3>3️⃣ Apply Migrations</h3>
<pre>
cd ../Celestial.Agency.DAL.EF
dotnet ef database update
</pre>

<h3>4️⃣ Run the API</h3>
<pre>
cd ../Celestial.Agency.Api
dotnet run
</pre>

<p>➡️ API will be available at: <b>https://localhost:5001/api</b></p>

<hr>

<h2>🧩 Example Endpoints</h2>

<h3>Blog</h3>
<pre>
GET  /api/en/Blog/GetBlogsList
GET  /api/en/Blog/GetBlogBySlug?slug=example
</pre>

<h3>Comment</h3>
<pre>
GET  /api/en/Comment/GetCommentBySlug
POST /api/en/Comment/AddComment
</pre>

<h3>Hotel</h3>
<pre>
GET  /api/en/Hotel/GetHotelById?id=1
</pre>

<hr>

<h2>📦 Modules</h2>

<ul>
  <li>✅ Blog Module</li>
  <li>✅ Comment Module</li>
  <li>✅ Hotel Module</li>
  <li>🛠️ Admin Dashboard (in progress)</li>
  <li>🛠️ Identity Management (in progress)</li>
  <li>🛠️ Advanced Localization (in progress)</li>
  <li>🛠️ Testing Layer (planned)</li>
</ul>

<hr>

<h2>💬 Contribution</h2>
<ol>
  <li>Fork this repository</li>
  <li>Create a feature branch: <code>git checkout -b feature/my-feature</code></li>
  <li>Commit and push your changes</li>
  <li>Open a Pull Request 🚀</li>
</ol>

<hr>

<h2>📜 License</h2>
<p>This project is licensed under the <b>MIT License</b>.  
You’re free to use, modify, and distribute this software with proper credit.</p>

<hr>

<h2>🌠 Author</h2>
<p>
  <strong>Sadra Khoshnavaz</strong><br>
  Open-source Contributor & Developer of the Celestial Series<br><br>
  📧 khoshnavazsadra@gmail.com<br>
  🔗 <a href="https://github.com/GhoostSK" target="_blank">github.com/GhoostSK</a>
</p>
