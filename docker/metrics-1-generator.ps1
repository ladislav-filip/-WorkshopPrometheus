# Nastavení cesty k souboru
$souborCesta = ".\nginx\html\metrics-1.txt"

# Funkce pro náhodné generování hodnot
function Get-RandomValues {
    $workshopLongRunning = Get-Random -Minimum 1 -Maximum 51
    $workshopVisitCount = Get-Content $souborCesta | Select-String "workshop_visit_count_total" | ForEach-Object { $_ -replace '\D', '' }
    $workshopVisitCount = [int]$workshopVisitCount + (Get-Random -Minimum 2 -Maximum 21)

    return $workshopLongRunning, $workshopVisitCount
}

# Nekonečná smyčka
while ($true) {
    # Získat náhodné hodnoty
    $newWorkshopLongRunning, $newWorkshopVisitCount = Get-RandomValues

    # Aktualizovat soubor s novými hodnotami a použít formát UNIX konců řádků
    $newContent = @(
        "workshop_long_running_gauge $newWorkshopLongRunning",
        "workshop_visit_count_total $newWorkshopVisitCount"
    )
    $newContent -join "`n" | Set-Content -Path $souborCesta -NoNewline

    Write-Host "Hodnoty byly uspesne zmeneny:"
    echo "   workshop_long_running_gauge $newWorkshopLongRunning"
    echo "   workshop_visit_count_total $newWorkshopVisitCount"

    # Přestávka 15 sekund
    Start-Sleep -Seconds 15
}
