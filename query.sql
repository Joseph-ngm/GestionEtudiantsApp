INSERT INTO Etudiants (Nom, Prenom, DateNaissance, Groupe)
VALUES 
('Easton', 'Doris', '2000-05-14', 'Groupe A'),
('Diallo', 'Mamadou', '1999-11-02', 'Groupe B'),
('Santos', 'Maria', '2001-03-27', 'Groupe A');

INSERT INTO Matieres (NomMatiere, Coefficient)
VALUES
('Mathématiques', 3),
('Programmation C#', 4),
('Bases de données SQL', 2);

INSERT INTO Notes (IdEtudiant, IdMatiere, Note, Session)
VALUES
(1, 1, 15.5, 'Normale'),
(2, 2, 18.0, 'Normale'),
(3, 3, 12.0, 'Rattrapage');
