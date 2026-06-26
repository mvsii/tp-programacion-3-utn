<?php
include 'conexion.php';

$tipo_doc = $_POST['tipo_doc'];
$documento = $_POST['documento'];
$nombre = $_POST['nombre'];
$apellido = $_POST['apellido'];
$fecha_nacimiento = $_POST['fecha_nacimiento'];
$email = $_POST['email'];
$usuario = $_POST['usuario'];
$passwordA = $_POST['passwordA'];
$passwordB = $_POST['passwordB'];

// Validacion de la password.
if (!($passwordA === $passwordB)) {
    echo "<h2> Las contraseñas no coinciden</h2>";
    echo "<a href='registro.html'> Volver </a>";
    exit();
}

// Validacion del tipo de documento para evitar request manual.
if (!($tipo_doc === 'PASAPORTE' || $tipo_doc === 'DNI')) {
    echo "<h2> El tipo de documento es invalido</h2>";
    echo "<a href='registro.html'> Volver </a>";
    exit();
}

$sql = "INSERT INTO usuarios (documento, tipo_doc, nombre, apellido, fecha_nacimiento, email, usuario, password) values ('$documento', '$tipo_doc', '$nombre', '$apellido', '$fecha_nacimiento', '$email', '$usuario', '$passwordA')";

if ($conn->query($sql) === true) {
    echo "<h2> La cuenta se creo correctamente</h2>";
    echo "<a href='ingreso.html'> Iniciar Sesion </a>";
} else {
    echo "<p>Error al intentar crear la cuenta: " . $conn->error;
}

$conn->close();