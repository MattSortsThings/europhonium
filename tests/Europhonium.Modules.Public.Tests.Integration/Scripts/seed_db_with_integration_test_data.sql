/*
 Executing this script seeds the database with the test data to be used by all integration tests in this assembly.

 The script must be run exactly once on the containerized test database, before any tests are run.
 */

-- 1. Populate [country] table

BEGIN TRANSACTION;

INSERT INTO dbo.country (id, country_code, name)
VALUES (N'4C096AFC-E568-4992-B700-15EDE2217571', N'GB', N'United Kingdom');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'368B347C-F756-4E65-8635-16FCF07AF0DF', N'HR', N'Croatia');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'60EA99FC-DEEF-4AA4-B5C9-608A6364F346', N'BE', N'Belgium');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'55FF33DD-D3C2-4B29-868C-733A95BB6518', N'CZ', N'Czechia');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'72442C41-969C-4FE9-A5BB-754AEC0C2E95', N'FI', N'Finland');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'0DC9EC9E-6FDA-44B1-BFB8-89123A795F6E', N'EE', N'Estonia');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'FEA58575-ACE1-4A34-A586-B64BDDBE6228', N'XX', N'Rest of World');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'2F3FC905-A946-48AE-9438-C4CBF43682C3', N'DE', N'Germany');
INSERT INTO dbo.country (id, country_code, name)
VALUES (N'669B6BAD-E9A9-4A9F-8965-DE9C478F0C36', N'AT', N'Austria');

COMMIT;
