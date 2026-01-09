Power Consumption of Robotic Solutions
Case descrip(cid:415)on
Robo(cid:415)c companies must consider power consump(cid:415)on in several regards, e.g., to op(cid:415)mize opera(cid:415)ng (cid:415)me
for ba(cid:425)ery-powered robots or to minimize the sustainability footprint of robo(cid:415)c solu(cid:415)ons. The power
consump(cid:415)on of a robot is made up of a complex combina(cid:415)on of use by actuators, sensors, computers and
communica(cid:415)on interfaces. These elements can both be on the robot or be part of suppor(cid:415)ng
infrastructure, e.g., to train AI models and run simula(cid:415)ons.
Companies would like to improve opera(cid:415)ng (cid:415)me as this drives up the business case for robo(cid:415)c solu(cid:415)ons.
Companies also need to consider the sustainability footprint associated with power use due to increasing
requirements for sustainability repor(cid:415)ng.
Drone examples for repeated flights with different loads
Dataset example: h(cid:425)ps://www.nature.com/ar(cid:415)cles/s41597-021-00930-x
Cobot examples for different movement speeds
Dataset example: h(cid:425)ps://ieee-dataport.org/documents/dataset-energy-assessment-collabora(cid:415)ve-robots

---

Challenge
Develop a so(cid:332)ware tool to help developers and/or integrators to understand/minimize the power
consump(cid:415)on of robo(cid:415)c solu(cid:415)ons including their infrastructure.
To get the work started, consider the following:
 Which elements of a robot solu(cid:415)on do you consider par(cid:415)cularly relevant to op(cid:415)mize?
 Would you focus on ba(cid:425)ery-powered robots or consider their total sustainability footprint?
 Will your so(cid:332)ware tool support developers in depth knowledge or provide integrators with
overviews?
Keywords
Robot/Drone power op(cid:415)miza(cid:415)on, energy efficiency, energy recovery
Tools, methods and materials
The challenge can be addressed with many different methods for data collec(cid:415)on, processing and
visualiza(cid:415)on. The data can put different focus on so(cid:332)ware or hardware elements. Methods can be
considered that could alter the programming of the solu(cid:415)on or change off-load pa(cid:425)erns or perform trade-
offs, e.g., accuracy vs. power consump(cid:415)on.
From SDU, the team will receive links to relevant datasets and so(cid:332)ware frameworks. An edge compu(cid:415)ng
setup with power monitoring is available at the Industry 4.0 lab. In addi(cid:415)on, SDU will provide feedback on
challenge elements.

---

Scalable Computing for Robotic Solutions
Case descrip(cid:415)on
Further advancement in making robo(cid:415)c solu(cid:415)ons more adaptable and intelligent depends on the ability to
execute complex simula(cid:415)ons for design explora(cid:415)on, synthe(cid:415)c data genera(cid:415)on, train large machine learning
models and run planning algorithms over many parameters. This results in increasing workloads that
robo(cid:415)c companies must cover the cost of. Scalability is the ability of a so(cid:332)ware system to sustain increasing
workloads by making use of addi(cid:415)onal resources.
To op(cid:415)mize the business case for robo(cid:415)c solu(cid:415)ons compu(cid:415)ng cost has to be minimized to the extent
possible. Therefore, efficient scalability is of vital importance for making robo(cid:415)c solu(cid:415)ons economic viable.
Nvidia robot controls system design architecture
Nvidia Isacc Sim resource check applica(cid:415)on

---

Challenge
Develop a so(cid:332)ware method or tool to op(cid:415)mize the compu(cid:415)ng use of robo(cid:415)c workloads in general or a
specific workload, e.g. simula(cid:415)on, machine learning, etc.
To get the work started, consider the following:
 What workloads would you like to focus on?
 Would you like to focus on distributed workloads or workloads on a single node distributed over
CPU and GPU loads?
 How is it possible to do sensible trade-offs between accuracy vs. cost (e.g. simula(cid:415)on steps, physics
accuracy…)
 How do you relate workloads to cost?
Keywords
Distributed compu(cid:415)ng, SaaS
Tools, methods and materials
The challenge can be addressed with many different methods considering benchmarking, so(cid:332)ware
architecture or distributed compu(cid:415)ng. The methods can put different focus on so(cid:332)ware or hardware
elements. Methods can be considered that could alter the programming of the solu(cid:415)on or change off-load
pa(cid:425)erns or perform trade-offs, e.g., accuracy vs. cost.
From SDU, the team will receive introduc(cid:415)on to relevant so(cid:332)ware frameworks. Different compu(cid:415)ng
op(cid:415)ons are available at SDU that can be used for experiments. In addi(cid:415)on, SDU will provide feedback on
challenge elements.

---

Developing a Multi-User VR System for Shared Physical Spaces
Case Description
Enterprise VR rooms are transforming physical spaces into shared multi-user VR environments
where teams can train together, with companies like Virtualware deploying over 32 VIROO® Rooms
worldwide for industries from railways to military training. However, most solutions require
expensive custom installations. This project aims to develop an a(cid:431)ordable VR system that enables
multiple users to share the same physical room while collaborating in a virtual environment. Unlike
typical multi-user VR platforms where users connect from di(cid:431)erent locations, this system
addresses the unique challenges of co-located users. Students will identify potential customers in
training facilities, educational labs, or team-building venues who could benefit from shared-space
VR experiences.
Challenge
The main challenge is creating a stable multi-user VR system where 3 users can operate in the same
physical room. While controllers are paired to individual headsets so there's no signal mixing,
students must investigate whether Meta Quest 3's inside-out tracking handles multiple headsets in
close proximity without interference. The system needs to manage shared physical boundaries,
prevent collisions between users who can't see each other, and ensure virtual avatars properly align
with users' actual positions. Students must implement basic networking to synchronize the virtual
environment while maintaining awareness of real-world space constraints. They will identify which
specific use cases benefit from users being physically co-located versus remotely connected.
Keywords
Virtual Reality, Co-located VR, Shared Physical Space, Multi-user Systems, Unity
Tools, methods and materials
The project will involve programming in C# and working with Unity development. Students will use
Unity's built-in Netcode for GameObjects networking solution along with VR frameworks like XR
Interaction Toolkit or OpenXR. Development will focus on solving the technical and safety
challenges of multiple headsets operating in the same room. Three Meta Quest 3s will be provided
for testing and development. Code management through GitHub with demonstrations throughout
the semester.

---

Developing VR Training Applications with Force Feedback Gloves
Case Description
The haptic technology market is expected to reach a value of $5 billion by 2028, with force feedback
gloves becoming essential for enterprise VR training. Force feedback simulates object hardness,
weight, and inertia, enabling trainees to feel the actual weight and resistance of virtual objects. Unlike
basic haptic vibrations, force feedback gloves physically restrict finger movement to simulate holding
heavy or solid objects. This project aims to develop specialized VR training applications using
SenseGlove technology that provides resistance up to 20N of force, equivalent to a 2kg brick on each
finger. SenseGlove is already being used by industry leaders including Volkswagen for assembly
training, creating opportunities for specialized applications in smaller organizations. Students will
identify and engage with potential customers in manufacturing assembly, medical training, or
technical education sectors who need realistic weight simulation for e(cid:431)ective training.
Challenge
The main challenge is creating training applications where feeling weight and resistance directly
improves learning outcomes. Students must integrate SenseGlove hardware with Unity and Meta
Quest 2, then develop algorithms that translate virtual object properties (mass, density, material
sti(cid:431)ness) into appropriate force resistance. The system must simulate realistic weight distribution
when lifting objects, resistance when turning tools like wrenches, or the varying density of di(cid:431)erent
materials. Students will create 1-2 demonstration scenarios (e.g., assembly line training where
workers feel component weight, medical training where surgeons feel tissue resistance, or tool
operation where users feel proper torque). They will validate which weight-based interactions provide
the most training value for their chosen market, targeting organizations that need cost-e(cid:431)ective
alternatives to enterprise solutions.
Keywords
Virtual Reality, Force Feedback, Weight Simulation, Training Simulation, Unity, SenseGlove, Haptic
Resistance.
Tools, methods and materials
The project will involve programming in C# and working with Unity development. Students will use
the SenseGlove SDK for Unity integration along with VR frameworks like XR Interaction Toolkit or
OpenXR. Development will focus on creating realistic weight and resistance simulations that
enhance muscle memory and procedural learning. One SenseGlove Nova and one Meta Quest 2
headset will be provided for testing and development. Students will identify potential customers and
validate market needs through stakeholder analysis. Code management through GitHub with
demonstrations throughout the semester.

---

Dansk: En optimering af FC0 (FC1-FC5) kalkulationen
DAO
English: An optimization of the FC0 (FC1-FC5) calculation
Case Description
Dansk: Undersøgelser af dao's Costmodel beregning har vist, at der er tale om komplekse
beregninger. I dag tager en beregning mellem 6-8 timer og kræver, at data opdeles i Øst/Vest
beregninger. Derudover involverer processen en del manuelt arbejde både før og efter selve
beregningen. Formålet med projektet er at undersøge mulighederne for at understøtte løn- og
omkostningsberegningen med mere e(cid:431)ektive redskaber i fremtiden.
English: An investigation into our Costmodel calculation has made it clear that these are complex
calculations. As it works today, a calculation takes between 6-8 hours and is dependent on data being
divided into East / West calculations. Prior to the calculations, a lot of manual work is done both
before and after the calculation. The purpose is to investigate what options there are to support salary
and cost calculations with more e(cid:431)icient .ools in the future.
Challenge
Dansk: Udfordringen er at optimere beregningen markant, potentielt via strømlining af data input,
begrænset udfaldsrum, eller andre algoritmiske optimeringer. En central del af opgaven er at vælge
et bedre og mere e(cid:431)ektivt koordinatsystem end det nuværende GPS-baserede WGS84, som kræver
meget komplekse beregninger til afstande. Løsningen skal kunne hente data fra forskellige systemer
og skal kunne kommunikere med en eksisterende besked-kø. Selvom fuld integration med det interne
ERP-system ikke er et krav i opgaven, skal løsningens design tage højde for det. Den endelige løsning
skal overholde eksisterende design- og kodestandarder, herunder SOLID-principperne.
English: The challenge is to greatly optimize this calculation, possibly through streamlining data input,
limiting the sample space, or other algorithmic technical optimizations. A key part of the task will be
to choose a better coordinate system than the current GPS-based WGS84, which is ine(cid:431)icient and
requires very complex calculations for determining distances. The solution must be able to collect
data from di(cid:431)erent systems with di(cid:431)erent interfaces and communicate with an existing message
queue. While integration with the internal ERP system is not a requirement in the project, the
solution's design must account for it. The developed solution must comply with existing design and
coding standards, including SOLID.
Keywords
Dansk: Optimering, Costmodel, Dataintegration, Koordinatsystem, Algoritmer, AI.
English: Optimization, Costmodel, Data Integration, Coordinate System, Algorithms, AI.
Tools, methods and materials
Dansk: De tekniske værktøjer, der skal anvendes, er Java, Spring Boot, RabbitMQ og Kubernetes.
English: The technical tools to be used are Java, Spring Boot, RabbitMQ, and Kubernetes.

---

Dansk: Optimering af adressedatabaser
DAO
English: Optimization of address databases
Case Description
Dansk: Den nuværende adressedatabase er kompleks, opbygget over lang tid og indeholder mange
manuelle kodemæssige operationer, der reducerer fleksibiliteten. Den kan desuden ikke håndtere
leverancer til erhvervskunder. Formålet er at undersøge, hvordan man kan opbygge en ny
adressedatabase, der understøtter rutedesign og leveringer i et samlet netværk med en optimal
datastruktur. Tesen er, at en graf-database kan opnå den ønskede søgningskompleksitet på O(n^2)
eller O(log n). Sekundært indgår beregninger på etageforhold i planlægningen.
English: The current address database is complex, has been built up over a long period of time, and
has many manual code-related operations that increase complexity and reduce flexibility.
Furthermore, it cannot handle deliveries to business customers. The purpose is to investigate how to
build an address database that can support route design and deliveries in a unified network, using a
data structure with optimal O(n^2) or O(log n) complexity for search and neighbor traversal. The thesis
is that by using a graph database, these searches can achieve that level of complexity. As a secondary
use case, include calculations on floor conditions in the planning model.
Challenge
Dansk: En central opgave er at undersøge og vælge en ny databaseteknologi, da dao ikke ønsker at
videreføre den nuværende Oracle-baserede løsning. Den nye løsning skal baseres på adresser fra
Danmarks Adresseregister via DAWA API'et. Der skal lægges stort fokus på at optimere læsning,
opdatering og berigelse af adresser, da databasen er central for mange systemer. Løsningen skal
kunne håndtere forskellige koordinatsystemer (både til GPS-rutevejledning og geokoordinater til
afstandsberegning) med fokus på fleksibilitet og optimering. En optionel udfordring er at undersøge
brugen af en specialiseret AI-model til at automatisere rettelsen af forkert indtastede adresser.
English: A core task is to research and select a new database technology, as dao does not want to
continue with the current Oracle-based solution. The new address solution must be based on the
Danish Address Register, using the DAWA API. It is essential to focus on optimizing the reading,
updating, and enriching of addresses, as the database is central to many systems. The database must
also accommodate the needs of di(cid:431)erent systems that use various coordinate systems (e.g., GPS for
route guidance vs. geocoordinates for distance calculation), focusing on both flexibility and
optimization. An optional challenge is to investigate using a specialized AI model to automate the
correction of incorrectly entered addresses, a task currently handled by manual resources.
Keywords
Dansk: Adressedatabase, Graf-database, Optimering, Søgealgoritmer, Dataintegration, AI, DAWA.
English: Address Database, Graph Database, Optimization, Search Algorithms, Data Integration, AI,
DAWA.
Tools, methods and materials
Dansk: De tekniske værktøjer, der skal anvendes, er Java, Spring Boot, RabbitMQ, GraphDb og
Kubernetes.
English: The technical tools to be used are Java, Spring Boot, RabbitMQ, GraphDb, and Kubernetes.

---

Codex-Agnosco: Local LLM-Assisted Legacy Code Analysis (VS Code Extension)
Case Description
Teams spend significant time understanding legacy code, and many cannot use code assistants due
to privacy constraints. This project ships a privacy-first VC Code extension that explains functions,
finds related code, and proposes docstrings using a local language model (no code leaves the
machine). Students will identify stakeholders such as enterprise development teams in regulated
industries and software modernization consultants who need on-prem assistance.
Challenge
Overcome single-file limits by architecting a repository-aware retrieval-augmented pipeline. Parse
the repository (e.g., with Roslyn) to extract symbols and structure; chunk meaningful units
(methods/classes); compute local embeddings; store them in a lightweight vector index; retrieve top-
k relevant code for a selection or question; and prompt a local code-centric LLM to generate
explanations, related-code summaries, and docstrings. Provide a VS Code extension UI that calls a
local HTTP service; keep performance acceptable on a typical developer laptop.
Keywords
Legacy Code, Static Analysis, Retrieval-Augmented Generation (RAG), Embeddings, Roslyn, VS Code
Extension, On-Prem AI
Tools, methods and materials
Extension frontend in Typescript; backend service in C# 12+ (.NET). Use Roslyn for parsing; generate
embeddings with a CPU-friendly sentence-transformers model; store in FAISS or SQLite. Run a small,
permissively licensed code model locally via Ollama or LM Studio (e.g., StarCoder2 or quantized
CodeLlama). Manage on GitHub with iterative demos and tests for indexing/retrieval quality.
Out of scope: multi-language support, model fine-tuning/training, telemetry.

---

Database Migration Safety Analyzer
Case Description
For any company running a production application, database migrations are high-stakes operations;
a single faulty script can cause data loss or downtime. Execution tools (e.g., Flyway) don’t analyze
safety. This project develops an open-source static analyzer that examines SQL migration scripts
before execution to detect dangerous patterns that could lead to data loss, performance degradation,
or irreversible changes. Students will identify potential users such as SaaS startups, digital agencies,
and SMB DevOps teams.
Challenge
Accurately model the impact of schema changes. Scope to PostgreSQL (MySQL as an optional
stretch). Build or extend a DDL parser, transform raw SQL into an AST, and implement a rule engine to
detect hazards (e.g., narrowing type changs, adding NOT NULL without a default, dropping populated
columns, foreign keys without supporting indexes, non-concurrent index builds, table-rewrite/lock-
risk operations). Track schema state across ordered migration files, rank findings by
severity/likelihood, and produce an actionable report and CI signal.
Keywords
Database Migration, Static Analysis, SQL Parsing, Schema Evolution, DevOps, PostgreSQL.
Tools, methods and materials
Implement in Python using sqlglot or similar, or in C# 12.0+ (.NET) with an ANTLR Postgres grammar.
Analyzer takes a directory of SQL files and emits a risk report (Markdown / HTML) plus a GitHub
Actions check that fails on high-risk migrations. Use Dockerized PostgreSQL for optional spot
validation. Manage on GitHub with unit tests over a seeded corpus.
Out of scope: cross-dialect support, DML/business-logic analysis, production database access.

---

Digital Accessibility Compliance Checker
Case Description
Digital accessibility is a legal and ethical imperative. Regulations such as the European Accessibility
Act and standards like WCAG create continuous demand for in-house audits that surface high-impact
issues and produce clear, shareable reports. This project delivers a lightweight, extensible auditing
platform; students will identify stakeholders such as university IT and compliance units, corporate
web teams, and disability services that require fast, reliable evidence of barriers and actionable
guidance.
Challenge
Design and build a robust, respectful scanning and reporting pipeline. The system must run a curated
subset of WCAG checks across a defined set of public URLs while applying privacy-by-design (persist
findings only, not page content). Engineer a resilient headless browser integration that honors
robots.txt and throttling; inject an accessibility engine; transform raw findings into an intelligent,
prioritized report by aggregating violations, ranking by a severity×frequency metric, linking each
finding to WCAG guidance, and suppressing obvious false positives to keep results actionable.
Keywords
Web Accessibility, WCAG, EN 301 549, Headless Browser, axe-core, Compliance Reporting, GDPR by
Design.
Tools, methods and materials
Implement in C# 12.0+ (.NET) or Python. Use Playwright for headless fetching and inject axe-core for
automated checks. Persist results to JSON or SQLite; generate a static HTML report (optional PDF via
wkhtmltopdf or WeasyPrint). Students select targets via a small config/CLI (sitemap or hand-picked
public pages). Manage code on GitHub with unit/functional tests focused on aggregation/ranking and
short iterative demos.
Out of scope: authenticated areas, custom rule authoring, and automatic fixes.
