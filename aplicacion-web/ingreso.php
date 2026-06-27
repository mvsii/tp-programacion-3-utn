<?php
include 'conexion.php';

$tipo_doc = $_POST['tipo_doc'];
$documento = $_POST['documento'];
$usuario = $_POST['usuario'];
$password = $_POST['password'];

// Consulta para validar si el usuario esta registrado
$sql = "SELECT documento, nombre, password 
        FROM usuarios 
        WHERE documento = '$documento'";

$result = $conn->query($sql);

$fila = $result->fetch_assoc();

// Validacion rapida
if (!$fila) {
    echo "<p>El documento no esta asociado a ningun usuario";
    exit();
}

// Iniciando session y guardando estado
if ($fila['password'] === $password) {
    session_start();
    $_SESSION['documento'] = $fila['documento'];
    $_SESSION['nombre'] = $fila['nombre'];
    $_SESSION['logueado'] = true;

    header("Location: resumen.php");
    exit();
} else {
    echo "<p>Contraseña incorrecta</p>";
}