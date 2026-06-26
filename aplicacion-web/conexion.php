<?php
$servername = "db_UTN";
$username = "root";
$password = "root";
$dbname = "mi_banco_db";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Error al conectarse a la base de datos: " . $conn->connect_error);
}