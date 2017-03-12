--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.1

-- Started on 2017-02-27 11:56:39

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 41321)
-- Name: a2bb_api; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA a2bb_api;


ALTER SCHEMA a2bb_api OWNER TO postgres;

SET search_path = a2bb_api, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 186 (class 1259 OID 41322)
-- Name: device; Type: TABLE; Schema: a2bb_api; Owner: postgres
--

CREATE TABLE device (
    id integer NOT NULL,
    user_id text NOT NULL,
    enabled boolean NOT NULL,
    name text NOT NULL
);


ALTER TABLE device OWNER TO postgres;

--
-- TOC entry 187 (class 1259 OID 41328)
-- Name: device_id_seq; Type: SEQUENCE; Schema: a2bb_api; Owner: postgres
--

CREATE SEQUENCE device_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE device_id_seq OWNER TO postgres;

--
-- TOC entry 2153 (class 0 OID 0)
-- Dependencies: 187
-- Name: device_id_seq; Type: SEQUENCE OWNED BY; Schema: a2bb_api; Owner: postgres
--

ALTER SEQUENCE device_id_seq OWNED BY device.id;


--
-- TOC entry 191 (class 1259 OID 41378)
-- Name: granter; Type: TABLE; Schema: a2bb_api; Owner: postgres
--

CREATE TABLE granter (
    id text NOT NULL,
    sub_id text
);


ALTER TABLE granter OWNER TO postgres;

--
-- TOC entry 188 (class 1259 OID 41330)
-- Name: in_out; Type: TABLE; Schema: a2bb_api; Owner: postgres
--

CREATE TABLE in_out (
    id integer NOT NULL,
    type integer NOT NULL,
    device_id integer NOT NULL,
    on_date timestamp with time zone
);


ALTER TABLE in_out OWNER TO postgres;

--
-- TOC entry 189 (class 1259 OID 41333)
-- Name: in_out_id_seq; Type: SEQUENCE; Schema: a2bb_api; Owner: postgres
--

CREATE SEQUENCE in_out_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE in_out_id_seq OWNER TO postgres;

--
-- TOC entry 2154 (class 0 OID 0)
-- Dependencies: 189
-- Name: in_out_id_seq; Type: SEQUENCE OWNED BY; Schema: a2bb_api; Owner: postgres
--

ALTER SEQUENCE in_out_id_seq OWNED BY in_out.id;


--
-- TOC entry 190 (class 1259 OID 41335)
-- Name: subject; Type: TABLE; Schema: a2bb_api; Owner: postgres
--

CREATE TABLE subject (
    id text NOT NULL
);


ALTER TABLE subject OWNER TO postgres;

--
-- TOC entry 2019 (class 2604 OID 41341)
-- Name: device id; Type: DEFAULT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY device ALTER COLUMN id SET DEFAULT nextval('device_id_seq'::regclass);


--
-- TOC entry 2020 (class 2604 OID 41342)
-- Name: in_out id; Type: DEFAULT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY in_out ALTER COLUMN id SET DEFAULT nextval('in_out_id_seq'::regclass);


--
-- TOC entry 2022 (class 2606 OID 41344)
-- Name: device device_pk; Type: CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY device
    ADD CONSTRAINT device_pk PRIMARY KEY (id);


--
-- TOC entry 2024 (class 2606 OID 41346)
-- Name: in_out in_out_pk; Type: CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY in_out
    ADD CONSTRAINT in_out_pk PRIMARY KEY (id);


--
-- TOC entry 2028 (class 2606 OID 41385)
-- Name: granter pk_granter; Type: CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY granter
    ADD CONSTRAINT pk_granter PRIMARY KEY (id);


--
-- TOC entry 2026 (class 2606 OID 41348)
-- Name: subject subject_pk; Type: CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY subject
    ADD CONSTRAINT subject_pk PRIMARY KEY (id);


--
-- TOC entry 2029 (class 2606 OID 41349)
-- Name: device device_subject_fk; Type: FK CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY device
    ADD CONSTRAINT device_subject_fk FOREIGN KEY (user_id) REFERENCES subject(id);


--
-- TOC entry 2031 (class 2606 OID 41391)
-- Name: granter granter_subject_fk; Type: FK CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY granter
    ADD CONSTRAINT granter_subject_fk FOREIGN KEY (sub_id) REFERENCES subject(id);


--
-- TOC entry 2030 (class 2606 OID 41354)
-- Name: in_out in_out_device_fk; Type: FK CONSTRAINT; Schema: a2bb_api; Owner: postgres
--

ALTER TABLE ONLY in_out
    ADD CONSTRAINT in_out_device_fk FOREIGN KEY (device_id) REFERENCES device(id);


-- Completed on 2017-02-27 11:56:39

--
-- PostgreSQL database dump complete
--

