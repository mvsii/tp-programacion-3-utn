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

// Validacion de la password
if (!($passwordA === $passwordB)) {
    echo "<h2> Las contraseñas no coinciden</h2>";
    echo "<a href='registro.html'> Volver </a>";
    exit();
}

// Validacion del tipo de documento para evitar request manual
if (!($tipo_doc === 'PASAPORTE' || $tipo_doc === 'DNI')) {
    echo "<h2> El tipo de documento es invalido</h2>";
    echo "<a href='registro.html'> Volver </a>";
    exit();
}

// Consulta para validar si el usuario tiene una tarjeta
$sql = "SELECT dni_titual
        FROM tarjetas
        WHERE dni_titular = '$documento'";

$result = $conn->query($sql);

$fila = $result->fetch_assoc();

// Validacion rapida
if (!$fila) {
    echo "<p>El documento no esta asociado a ningun usuario";
    echo "<a href='registro.html'> Volver al registro </a>";
    exit();
}

// Consulta para modificar y terminar de registrar el usuario
$sql = "UPDATE usuarios 
        SET usuario = '$usuario', password = '$passwordA' 
        WHERE documento = '$documento'";

if ($conn->query($sql) === true) {
    echo "<h2> La cuenta se registro correctamente</h2>";
    echo "<a href='ingreso.html'> Iniciar Sesion </a>";
} else {
    echo "<p>Error al intentar crear la cuenta: " . $conn->error;
}

$conn->close();