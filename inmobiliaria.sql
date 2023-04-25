-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 25-04-2023 a las 20:06:41
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `monto_alquiler` decimal(10,0) NOT NULL,
  `vigente` tinyint(4) DEFAULT 1,
  `borrado` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish2_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInmueble`, `idInquilino`, `fecha_inicio`, `fecha_fin`, `monto_alquiler`, `vigente`, `borrado`) VALUES
(37, 1, 2, '2023-04-02', '2023-04-19', '1551', 1, 0),
(38, 1, 2, '2023-04-12', '2023-04-28', '599595', 1, 0),
(39, 1, 2, '2023-03-28', '2023-04-28', '666', 1, 0),
(40, 1, 2, '2023-04-11', '2023-04-28', '1444', 1, 0),
(41, 1, 2, '2023-04-09', '2023-04-27', '5555', 1, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `idPropietario` int(11) DEFAULT NULL,
  `direccion` varchar(50) NOT NULL,
  `tipo` varchar(50) NOT NULL,
  `uso` varchar(50) NOT NULL,
  `ambientes` int(11) NOT NULL,
  `precio` decimal(10,0) NOT NULL,
  `disponible` int(11) DEFAULT 1,
  `zona` varchar(50) DEFAULT NULL,
  `latitud` varchar(50) NOT NULL,
  `longitud` varchar(50) NOT NULL,
  `borrado` tinyint(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish2_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `idPropietario`, `direccion`, `tipo`, `uso`, `ambientes`, `precio`, `disponible`, `zona`, `latitud`, `longitud`, `borrado`) VALUES
(1, 9, 'Modulo 12 manzana 30 casa 15', 'Casa', 'Comercial', 2, '123123', 1, NULL, '-36.55107061919364', '-423.7984821787318', 0),
(9, 10, 'la lora', 'Local', 'Residencial', 3, '1312312', 1, NULL, '-33.29021633374792', '-66.31617754697801', 0),
(14, 11, 'San Luis', 'Local', 'Residencial', 2, '222222222', 1, NULL, '-33.303129652036475', '-66.33666962385179', 0),
(15, 9, 'Buenos aires', 'Local', 'Residencial', 3, '131231', 1, NULL, '-34.7506398050501', '-58.53635787963868', 0),
(16, 13, 'junin y algo mas', 'Local', 'Residencial', 3, '1231', 1, NULL, '-33.29753411547317', '-66.33323639631273', 0),
(17, 14, 'Modulo 12asadsa 15', 'Local', 'Residencial', 3, '41333333', 1, NULL, '-33.29308612522569', '-66.32070511579515', 0),
(19, 11, 'Buenos aires 55', 'Local', 'Residencial', 2, '121212', 1, NULL, '-33.29366007219437', '-66.30508393049242', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `cuil_cuit` varchar(50) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `domicilio_trabajo` varchar(50) NOT NULL,
  `domicilio` varchar(50) NOT NULL,
  `nombre_garante` varchar(50) NOT NULL,
  `dni_garante` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish2_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `dni`, `cuil_cuit`, `nombre`, `apellido`, `telefono`, `email`, `domicilio_trabajo`, `domicilio`, `nombre_garante`, `dni_garante`) VALUES
(2, '41710461', '45648651', 'Robertito', 'Acevedo', '151015', 'dasdasdasda@gmail.com', 'Peron y mitre', 'La punta', 'Cacho', '00007896'),
(3, '4865486', '46548654865', 'Carlo', 'labolla', '49498498', 'pinpin@gmail.com', 'la quiaca', 'la paz', 'Verdulero', '4984984984'),
(4, '4984984984', '94984984984', 'Laura', 'Vovera', '4984984', 'claxclaxupad@gmail.com', 'acqua', 'las chacras', 'Chapulin', '4849948849848'),
(5, '40784685', '20481651656', 'Roberto', 'Carlo', '264565615', 'carlo@gmail.com', 'walmart', 'CGT', 'Veronica Diaz', '3879878486');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `cuota` decimal(10,0) NOT NULL,
  `nro_cuota` int(11) NOT NULL,
  `fecha_pago` date NOT NULL,
  `anulado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish2_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `domicilio` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish2_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `dni`, `nombre`, `apellido`, `domicilio`, `telefono`, `email`) VALUES
(9, '41710461', 'Ezequiel', 'Acevedo', 'M12 M30 C15', '150151231', 'ezeelkpo2017@gmail.com'),
(10, '40987456', 'Jonatan', 'Salas', 'M12 M32 C20', '9874513219', 'salasjohn@gmail.com'),
(11, '38451687', 'Gaston', 'Hyouka', 'Fuerte apache', '491845618465', 'hyoukasempai@gmail.com'),
(12, '42498465', 'Rodrigo', 'Torres', 'M12 M31 C14', '46548446516', 'zepekeño@gmail.com'),
(13, '38485156', 'Obi Wan', 'Diaz', 'Serranias puntanas', '1290301298312', 'elpoderdelafuerza@gmail.com'),
(14, '43548865', 'Nehuen', 'Ferrero', 'LIC 22 M31 C12', '19819849', 'plantasanta@gmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `avatar` varchar(100) DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `clave` varchar(50) NOT NULL,
  `rol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish2_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `nombre`, `apellido`, `avatar`, `email`, `clave`, `rol`) VALUES
(10, 'Eze', 'Acevedo', '/imgPerfil\\avatar_10.png', 'muzza@gmail.com', 'cxuLPg0KBjzhcL4qrUYtr0N3S1O/ObA1rj4sMTy50+Y=', 1),
(12, 'Pedrote', 'Peral', '/imgPerfil\\avatar_12.png', 'fiero@gmail.com', 'e2nwtldgmtEilYxBKwWg0fihMN9c8TSL+EQMYqzYKPI=', 3),
(14, 'Roberto', 'Gordo', '/imgPerfil\\avatar_14.jpeg', 'eze@eze.com', 'e2nwtldgmtEilYxBKwWg0fihMN9c8TSL+EQMYqzYKPI=', 2);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `inquilino` (`idInquilino`),
  ADD KEY `inmueble` (`idInmueble`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `idPropietario` (`idPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `contrato` (`idContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `inmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `inquilino` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `contrato` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
