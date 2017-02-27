--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.1

-- Started on 2017-02-26 18:44:44

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 41130)
-- Name: a2bb_idsrv; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA a2bb_idsrv;


ALTER SCHEMA a2bb_idsrv OWNER TO postgres;

--
-- TOC entry 1 (class 3079 OID 12387)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2195 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = a2bb_idsrv, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 186 (class 1259 OID 41131)
-- Name: AspNetRoleClaims; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetRoleClaims" (
    "Id" integer NOT NULL,
    "ClaimType" text,
    "ClaimValue" text,
    "RoleId" text NOT NULL
);


ALTER TABLE "AspNetRoleClaims" OWNER TO postgres;

--
-- TOC entry 187 (class 1259 OID 41137)
-- Name: AspNetRoles; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetRoles" (
    "Id" text NOT NULL,
    "ConcurrencyStamp" text,
    "Name" character varying(256),
    "NormalizedName" character varying(256)
);


ALTER TABLE "AspNetRoles" OWNER TO postgres;

--
-- TOC entry 188 (class 1259 OID 41143)
-- Name: AspNetUserClaims; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetUserClaims" (
    "Id" integer NOT NULL,
    "ClaimType" text,
    "ClaimValue" text,
    "UserId" text NOT NULL
);


ALTER TABLE "AspNetUserClaims" OWNER TO postgres;

--
-- TOC entry 189 (class 1259 OID 41149)
-- Name: AspNetUserLogins; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" text NOT NULL,
    "ProviderKey" text NOT NULL,
    "ProviderDisplayName" text,
    "UserId" text NOT NULL
);


ALTER TABLE "AspNetUserLogins" OWNER TO postgres;

--
-- TOC entry 190 (class 1259 OID 41155)
-- Name: AspNetUserRoles; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL
);


ALTER TABLE "AspNetUserRoles" OWNER TO postgres;

--
-- TOC entry 191 (class 1259 OID 41161)
-- Name: AspNetUserTokens; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text
);


ALTER TABLE "AspNetUserTokens" OWNER TO postgres;

--
-- TOC entry 192 (class 1259 OID 41167)
-- Name: AspNetUsers; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "AspNetUsers" (
    "Id" text NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    "ConcurrencyStamp" text,
    "Email" character varying(256),
    "EmailConfirmed" boolean NOT NULL,
    "LockoutEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "NormalizedEmail" character varying(256),
    "NormalizedUserName" character varying(256),
    "PasswordHash" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "SecurityStamp" text,
    "TwoFactorEnabled" boolean NOT NULL,
    "UserName" character varying(256)
);


ALTER TABLE "AspNetUsers" OWNER TO postgres;

--
-- TOC entry 193 (class 1259 OID 41173)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: a2bb_idsrv; Owner: postgres
--

CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE "__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 2181 (class 0 OID 41131)
-- Dependencies: 186
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--



--
-- TOC entry 2182 (class 0 OID 41137)
-- Dependencies: 187
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--

INSERT INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "Name", "NormalizedName") VALUES ('8e77511a-f3e6-4c30-b9aa-d77686460d31', NULL, 'Admin', 'ADMIN');
INSERT INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "Name", "NormalizedName") VALUES ('f619c713-ef47-45b6-b78c-1026237408b0', NULL, 'User', 'USER');


--
-- TOC entry 2183 (class 0 OID 41143)
-- Dependencies: 188
-- Data for Name: AspNetUserClaims; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--



--
-- TOC entry 2184 (class 0 OID 41149)
-- Dependencies: 189
-- Data for Name: AspNetUserLogins; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--



--
-- TOC entry 2185 (class 0 OID 41155)
-- Dependencies: 190
-- Data for Name: AspNetUserRoles; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--

INSERT INTO "AspNetUserRoles" ("UserId", "RoleId") VALUES ('07d9de72-3fd4-400a-861f-8331f2351720', '8e77511a-f3e6-4c30-b9aa-d77686460d31');


--
-- TOC entry 2186 (class 0 OID 41161)
-- Dependencies: 191
-- Data for Name: AspNetUserTokens; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--



--
-- TOC entry 2187 (class 0 OID 41167)
-- Dependencies: 192
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--

INSERT INTO "AspNetUsers" ("Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName") VALUES ('07d9de72-3fd4-400a-861f-8331f2351720', 0, NULL, NULL, true, true, NULL, NULL, 'ADMIN', 'AQAAAAEAACcQAAAAEPj+GyOUWfy6DX0G6NIFFCXt2pujSCHYBAb0GjM9g8LgIDZj9JvLHmezk72OjGmAMw==', NULL, true, NULL, false, 'Admin');


--
-- TOC entry 2188 (class 0 OID 41173)
-- Dependencies: 193
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: a2bb_idsrv; Owner: postgres
--

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('00000000000000_CreateIdentitySchema', '1.1.0-rtm-22752');
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20170210120008_PopulateDB', '1.1.0-rtm-22752');


--
-- TOC entry 2037 (class 2606 OID 41177)
-- Name: AspNetRoleClaims PK_AspNetRoleClaims; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetRoleClaims"
    ADD CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id");


--
-- TOC entry 2039 (class 2606 OID 41179)
-- Name: AspNetRoles PK_AspNetRoles; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetRoles"
    ADD CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id");


--
-- TOC entry 2043 (class 2606 OID 41181)
-- Name: AspNetUserClaims PK_AspNetUserClaims; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserClaims"
    ADD CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id");


--
-- TOC entry 2046 (class 2606 OID 41183)
-- Name: AspNetUserLogins PK_AspNetUserLogins; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserLogins"
    ADD CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey");


--
-- TOC entry 2050 (class 2606 OID 41185)
-- Name: AspNetUserRoles PK_AspNetUserRoles; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 2052 (class 2606 OID 41187)
-- Name: AspNetUserTokens PK_AspNetUserTokens; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserTokens"
    ADD CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- TOC entry 2055 (class 2606 OID 41189)
-- Name: AspNetUsers PK_AspNetUsers; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUsers"
    ADD CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id");


--
-- TOC entry 2058 (class 2606 OID 41191)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 2053 (class 1259 OID 41192)
-- Name: EmailIndex; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "EmailIndex" ON "AspNetUsers" USING btree ("NormalizedEmail");


--
-- TOC entry 2035 (class 1259 OID 41193)
-- Name: IX_AspNetRoleClaims_RoleId; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" USING btree ("RoleId");


--
-- TOC entry 2041 (class 1259 OID 41194)
-- Name: IX_AspNetUserClaims_UserId; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" USING btree ("UserId");


--
-- TOC entry 2044 (class 1259 OID 41195)
-- Name: IX_AspNetUserLogins_UserId; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" USING btree ("UserId");


--
-- TOC entry 2047 (class 1259 OID 41196)
-- Name: IX_AspNetUserRoles_RoleId; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" USING btree ("RoleId");


--
-- TOC entry 2048 (class 1259 OID 41197)
-- Name: IX_AspNetUserRoles_UserId; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "IX_AspNetUserRoles_UserId" ON "AspNetUserRoles" USING btree ("UserId");


--
-- TOC entry 2040 (class 1259 OID 41198)
-- Name: RoleNameIndex; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE INDEX "RoleNameIndex" ON "AspNetRoles" USING btree ("NormalizedName");


--
-- TOC entry 2056 (class 1259 OID 41199)
-- Name: UserNameIndex; Type: INDEX; Schema: a2bb_idsrv; Owner: postgres
--

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" USING btree ("NormalizedUserName");


--
-- TOC entry 2059 (class 2606 OID 41200)
-- Name: AspNetRoleClaims FK_AspNetRoleClaims_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetRoleClaims"
    ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 2060 (class 2606 OID 41205)
-- Name: AspNetUserClaims FK_AspNetUserClaims_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserClaims"
    ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2061 (class 2606 OID 41210)
-- Name: AspNetUserLogins FK_AspNetUserLogins_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserLogins"
    ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2062 (class 2606 OID 41215)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 2063 (class 2606 OID 41220)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: a2bb_idsrv; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


-- Completed on 2017-02-26 18:44:45

--
-- PostgreSQL database dump complete
--

