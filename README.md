# Examen Final Desarrollo WEB

**BASE DE DATOS (Esquema)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.001.png)


**BASE DE DATOS (Datos de muestra)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.002.png)![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.003.png)

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.004.png)

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.005.png)



**WEB SERVICES SOAP (DEFINICIÓN)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.006.png)



**WEB SERVICES SOAP (CONSUMO)**

**AgregarMusicoAGrupo**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.007.png)

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.008.png)

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.009.png)

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.010.png)



**ObtenerMusicoPorGenero**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.011.png)

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.012.png)

**ObtenerGrupoMasGenerosMusicales![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.013.png)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.014.png)

**ObtenerGruposIntegrantes![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.015.png)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.016.png)



**Scripts Base de Datos** 

**Todos los objetos de base de datos y permisos están asociados al esquema y usuario  EXAMEN_FINAL_DESA_WEB*.***

```sql
--DROP USER EXAMEN_FINAL_DESA_WEB CASCADE;

CREATE USER EXAMEN_FINAL_DESA_WEB IDENTIFIED BY "P@ssw0rd" ACCOUNT UNLOCK;

GRANT RESOURCE, CONNECT TO EXAMEN_FINAL_DESA_WEB;

GRANT UNLIMITED TABLESPACE TO EXAMEN_FINAL_DESA_WEB;
```

**02_CREACION_TABLAS**

```sql
ALTER SESSION SET CURRENT_SCHEMA = EXAMEN_FINAL_DESA_WEB;

-- Crear secuencias para las llaves primarias

CREATE SEQUENCE seq_idgenero START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE seq_idgrupo START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE seq_idmusico START WITH 1 INCREMENT BY 1;

-- Crear la tabla 'genero'

CREATE TABLE genero (

    idgenero INT DEFAULT seq_idgenero.NEXTVAL PRIMARY KEY,

    descripcion VARCHAR(45)

);

-- Crear la tabla 'grupo'

CREATE TABLE grupo (

    idgrupo INT DEFAULT seq_idgrupo.NEXTVAL PRIMARY KEY,

    nombre VARCHAR(45),

    formacion DATE,

    desintegracion DATE

);

-- Crear la tabla 'musico'

CREATE TABLE musico (

    idmusico INT DEFAULT seq_idmusico.NEXTVAL PRIMARY KEY,

    nombre VARCHAR(45),

    instrumento VARCHAR(45),

    lugarnacimiento VARCHAR(45),

    fechanacimiento DATE,

    fechamuerte DATE

);

-- Crear la tabla 'generosgrupos' sin restricciones de clave foránea

CREATE TABLE generosgrupos (

    idgrupo INT,

    idgenero INT

);

-- Crear la tabla 'musicosgrupos' sin restricciones de clave foránea

CREATE TABLE musicosgrupos (

    idgrupo INT,

    idmusico INT,

    instrumento VARCHAR(45),

    fechainicio DATE,

    fechafin DATE

);

-- Agregar las restricciones de clave foránea después de crear todas las tablas

ALTER TABLE generosgrupos

ADD CONSTRAINT fk_generosgrupos_grupo FOREIGN KEY (idgrupo) REFERENCES grupo(idgrupo);

ALTER TABLE generosgrupos

ADD CONSTRAINT fk_generosgrupos_genero FOREIGN KEY (idgenero) REFERENCES genero(idgenero);

ALTER TABLE musicosgrupos

ADD CONSTRAINT fk_musicosgrupos_grupo FOREIGN KEY (idgrupo) REFERENCES grupo(idgrupo);

ALTER TABLE musicosgrupos

ADD CONSTRAINT fk_musicosgrupos_musico FOREIGN KEY (idmusico) REFERENCES musico(idmusico);
```


**03_DATOS_MUESTRA**

```sql
-- Insertar registros de ejemplo en la tabla 'genero'

INSERT INTO genero (descripcion) VALUES ('Rock');

INSERT INTO genero (descripcion) VALUES ('Pop');

INSERT INTO genero (descripcion) VALUES ('Jazz');

INSERT INTO genero (descripcion) VALUES ('Hip-Hop');

-- Insertar registros de ejemplo en la tabla 'grupo'

INSERT INTO grupo (nombre, formacion, desintegracion) VALUES ('The Beatles', TO_DATE('1960-01-01', 'YYYY-MM-DD'), TO_DATE('1970-04-10', 'YYYY-MM-DD'));

INSERT INTO grupo (nombre, formacion, desintegracion) VALUES ('Queen', TO_DATE('1970-06-27', 'YYYY-MM-DD'), NULL);

INSERT INTO grupo (nombre, formacion, desintegracion) VALUES ('Led Zeppelin', TO_DATE('1968-09-01', 'YYYY-MM-DD'), TO_DATE('1980-12-04', 'YYYY-MM-DD'));

-- Insertar registros de ejemplo en la tabla 'musico'

INSERT INTO musico (nombre, instrumento, lugarnacimiento, fechanacimiento, fechamuerte) VALUES ('John Lennon', 'Guitarra', 'Liverpool', TO_DATE('1940-10-09', 'YYYY-MM-DD'), TO_DATE('1980-12-08', 'YYYY-MM-DD'));

INSERT INTO musico (nombre, instrumento, lugarnacimiento, fechanacimiento, fechamuerte) VALUES ('Freddie Mercury', 'Voz', 'Zanzíbar', TO_DATE('1946-09-05', 'YYYY-MM-DD'), TO_DATE('1991-11-24', 'YYYY-MM-DD'));

INSERT INTO musico (nombre, instrumento, lugarnacimiento, fechanacimiento, fechamuerte) VALUES ('Jimmy Page', 'Guitarra', 'Heston', TO_DATE('1944-01-09', 'YYYY-MM-DD'), NULL);

-- Insertar registros de ejemplo en la tabla 'generosgrupos'

INSERT INTO generosgrupos (idgrupo, idgenero) VALUES (1, 1);

INSERT INTO generosgrupos (idgrupo, idgenero) VALUES (1, 3);

INSERT INTO generosgrupos (idgrupo, idgenero) VALUES (2, 2);

INSERT INTO generosgrupos (idgrupo, idgenero) VALUES (3, 1);

INSERT INTO generosgrupos (idgrupo, idgenero) VALUES (3, 3);

-- Insertar registros de ejemplo en la tabla 'musicosgrupos'

INSERT INTO musicosgrupos (idgrupo, idmusico, instrumento, fechainicio, fechafin) VALUES (1, 1, 'Guitarra', TO_DATE('1960-01-01', 'YYYY-MM-DD'), TO_DATE('1970-04-10', 'YYYY-MM-DD'));

INSERT INTO musicosgrupos (idgrupo, idmusico, instrumento, fechainicio, fechafin) VALUES (2, 2, 'Voz', TO_DATE('1970-06-27', 'YYYY-MM-DD'), NULL);

INSERT INTO musicosgrupos (idgrupo, idmusico, instrumento, fechainicio, fechafin) VALUES (3, 3, 'Guitarra', TO_DATE('1968-09-01', 'YYYY-MM-DD'), TO_DATE('1980-12-04', 'YYYY-MM-DD'));

COMMIT;
```

**04_PAQUETE_EXAMEN_FINAL**

```sql
CREATE OR REPLACE PACKAGE ExamenFinal AS

    PROCEDURE AgregarMusicoAGrupo(

        p_IdMusico IN NUMBER,

        p_IdGrupo IN NUMBER,

        p_Instrumento IN VARCHAR2,

        p_Estado OUT VARCHAR2,

        p_DescripcionError OUT VARCHAR2

    );

END ExamenFinal;

/

CREATE OR REPLACE PACKAGE BODY ExamenFinal AS

    PROCEDURE AgregarMusicoAGrupo(

        p_IdMusico IN NUMBER,

        p_IdGrupo IN NUMBER,

        p_Instrumento IN VARCHAR2,    

        p_Estado OUT VARCHAR2,

        p_DescripcionError OUT VARCHAR2

    ) AS

        v_Contador NUMBER;

    BEGIN

        -- Inicializa los parámetros de salida

        p_Estado := 'EXITO';

        p_DescripcionError := NULL;



        SELECT COUNT(\*)

        INTO v_Contador

        FROM musicosgrupos

        WHERE idmusico = p_IdMusico AND idgrupo = p_IdGrupo;

    IF v_Contador = 0 THEN

        -- El músico no es miembro del grupo, así que lo agregamos

        INSERT INTO musicosgrupos (idmusico, idgrupo,instrumento,fechainicio)

        VALUES (p_idmusico, p_idgrupo, p_Instrumento,TRUNC(SYSDATE));

    ELSE

        p_Estado := 'ERROR';

        p_DescripcionError := 'El músico ya es miembro de este grupo.';

    END IF;

    EXCEPTION

        WHEN OTHERS THEN

        p_Estado := 'ERROR';

        p_DescripcionError := 'BD: ' || SQLERRM;

    END AgregarMusicoAGrupo;

END ExamenFinal;

/

```

**Solución .Net (WS SOAP)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.017.png)



**Repositorio GITHUB (Actualizado)**

![](20231104_DOCUMENTACION_EXAMEN_FINAL/Aspose.Words.4ba1352f-3a59-4626-8303-2f9ffa6dd019.018.png)


<https://github.com/josua-reyes/ExamenFinalDesarrolloWeb>


