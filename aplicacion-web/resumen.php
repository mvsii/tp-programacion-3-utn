<?php
session_start();

// Validacion de session
if (!isset($_SESSION['logueado']) || $_SESSION['logueado'] !== true) {
    header("Location: ingreso.php");
    exit();
}

include 'conexion.php';

// Guardo el documento en una variable para no repetirlo
$documento = $_SESSION['documento'];

// Consulta para traer las tarjetas del usuario
$sql_tarjetas = "SELECT * FROM tarjetas WHERE dni_titular = '$documento'";
$result_tarjetas = $conn->query($sql_tarjetas);
?>

<!doctype html>
<html lang="es">

<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Mis Tarjetas - Resumen</title>
    <script src="https://cdn.tailwindcss.com"></script>
</head>

<body class="bg-gray-100 font-sans min-h-screen flex flex-col justify-between">
    <header class="bg-[#004691] text-white text-center py-4 shadow-md">
        <div class="flex items-center justify-between max-w-5xl mx-auto px-4">
            <h1 class="text-xl font-semibold">
                Mis <span class="font-bold">Tarjetas</span>
            </h1>
            <div class="flex items-center gap-4">
                <span class="text-sm">Hola, <?php echo htmlspecialchars($_SESSION['nombre']); ?></span>
            </div>
        </div>
    </header>

    <main class="flex-grow max-w-5xl w-full mx-auto p-6">
        <h2 class="text-2xl font-bold text-[#004691] mb-6">Mis Tarjetas y Liquidaciones</h2>

        <div class="space-y-4" id="acordeon">
            <?php while ($tarjeta = $result_tarjetas->fetch_assoc()):
                $numCuenta = $tarjeta['num_cuenta'];
                // Consulta para traer las liquidaciones de cada tarjeta
                $sql_liq = "SELECT * FROM liquidaciones WHERE num_cuenta = '$numCuenta'";
                $result_liq = $conn->query($sql_liq);
                ?>
                <div class="bg-white rounded-lg shadow overflow-hidden">
                    <button onclick="toggleAcordeon(<?php echo $numCuenta; ?>)"
                        class="w-full flex items-center justify-between p-5 hover:bg-gray-50 transition text-left">
                        <div class="flex items-center gap-4">
                            <div
                                class="w-12 h-8 bg-gradient-to-br from-[#004691] to-blue-400 rounded flex items-center justify-center">
                                <svg class="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                        d="M3 10h18M7 15h1m4 0h1m-7 4h12a3 3 0 003-3V8a3 3 0 00-3-3H6a3 3 0 00-3 3v8a3 3 0 003 3z" />
                                </svg>
                            </div>
                            <div>
                                <p class="font-bold text-gray-800">
                                    <?php echo $tarjeta['banco_emisor']; ?> ••••
                                    <?php echo substr($tarjeta['numero_tarjeta'], -4); ?>
                                </p>
                                <p class="text-sm text-gray-500">
                                    <?php echo $tarjeta['estado']; ?> — Saldo:
                                    $<?php echo number_format($tarjeta['saldo'], 2, ',', '.'); ?>
                                </p>
                            </div>
                        </div>
                        <svg id="icono-<?php echo $numCuenta; ?>"
                            class="w-5 h-5 text-gray-400 transition-transform duration-200" fill="none"
                            stroke="currentColor" viewBox="0 0 24 24">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                        </svg>
                    </button>

                    <!-- Contenido del acordeon (se oculta con la clase hidden) -->
                    <div id="contenido-<?php echo $numCuenta; ?>" class="hidden border-t border-gray-100">
                        <div class="divide-y divide-gray-100">
                            <div
                                class="grid grid-cols-4 gap-4 px-5 py-2 bg-gray-50 text-xs font-semibold text-gray-500 uppercase">
                                <span>Período</span>
                                <span>Vencimiento</span>
                                <span class="text-right">Total a Pagar</span>
                                <span class="text-right">Pago Mínimo</span>
                            </div>
                            <?php while ($liq = $result_liq->fetch_assoc()): ?>
                                <div class="grid grid-cols-4 gap-4 px-5 py-3 hover:bg-gray-50 transition text-sm">
                                    <span class="font-medium text-gray-800"><?php echo $liq['periodo']; ?></span>
                                    <span
                                        class="text-gray-600"><?php echo date('d/m/Y', strtotime($liq['fecha_vencimiento'])); ?></span>
                                    <span
                                        class="text-right font-bold text-gray-800">$<?php echo number_format($liq['total_a_pagar'], 2, ',', '.'); ?></span>
                                    <span
                                        class="text-right text-gray-600">$<?php echo number_format($liq['pago_minimo'], 2, ',', '.'); ?></span>
                                </div>
                            <?php endwhile; ?>
                        </div>
                    </div>
                </div>
            <?php endwhile; ?>
        </div>
    </main>

    <footer class="bg-gray-50 text-[10px] text-gray-500 text-center p-4 border-t border-gray-200">
        Portal Oficial de Consultas de Liquidaciones Progra3card.
    </footer>

    <script>
        // Muestra y oculta las liquidaciones de cada tarjeta
        function toggleAcordeon(numCuenta) {
            const contenido = document.getElementById('contenido-' + numCuenta);
            const icono = document.getElementById('icono-' + numCuenta);

            if (contenido.classList.contains('hidden')) {
                contenido.classList.remove('hidden');
                icono.style.transform = 'rotate(180deg)';
            } else {
                contenido.classList.add('hidden');
                icono.style.transform = 'rotate(0deg)';
            }
        }
    </script>
</body>

</html>