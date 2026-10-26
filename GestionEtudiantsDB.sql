CREATE DATABASE GestionEtudiantsDB;
GO
USE GestionEtudiantsDB;
GO
CREATE TABLE Etudiants (IdEtudiant INT IDENTITY(1,1) PRIMARY KEY,
 Nom NVARCHAR(100) NOT NULL,
 Prenom NVARCHAR(100) NOT NULL,
 DateNaissance DATE,
 Groupe NVARCHAR(50)
);
GO
CREATE TABLE Matieres (
 IdMatiere INT IDENTITY(1,1) PRIMARY KEY,
 NomMatiere NVARCHAR(100) NOT NULL,
 Coefficient INT NOT NULL DEFAULT 1
);
GO
CREATE TABLE Notes (
 IdNote INT IDENTITY(1,1) PRIMARY KEY,
 IdEtudiant INT NOT NULL,
 IdMatiere INT NOT NULL,
 Note DECIMAL(4,2) CHECK (Note BETWEEN 0 AND 20),
 Session NVARCHAR(20) DEFAULT 'Normale',
 FOREIGN KEY (IdEtudiant) REFERENCES Etudiants(IdEtudiant),
 FOREIGN KEY (IdMatiere) REFERENCES Matieres(IdMatiere)
);
GOselect * from Notes