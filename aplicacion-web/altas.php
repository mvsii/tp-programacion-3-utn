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

$sql = "SELECT * FROM usuarios WHERE documento = '$documento'";

$resul = $conn->query($sql);

$fila = $resul->fetch_assoc();

if (!$fila) {
    echo "<p>El documento no esta asociado a ningun usuario";
    exit();
}

$sql = "UPDATE usuarios SET usuario = '$usuario', password = '$passwordA' where documento = '$documento'";

if ($conn->query($sql) === true) {
    echo "<h2> La cuenta se registro correctamente</h2>";
    echo "<a href='ingreso.html'> Iniciar Sesion </a>";
} else {
    echo "<p>Error al intentar crear la cuenta: " . $conn->error;
}

$conn->close();