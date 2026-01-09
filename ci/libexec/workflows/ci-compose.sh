#!/usr/bin/env bash
# shellcheck disable=SC2155

###
# Compose the project's artifacts, e.g., compiled binaries, Docker images.
#   This workflow performs all steps required to create the project's output.
#
#   This script expects a CICEE CI library environment (which is provided when using 'cicee lib exec').
#   For CI library environment details, see: https://github.com/JeremiahSanders/cicee/blob/main/docs/use/ci-library.md
#
# How to use:
#   Modify the "ci-compose" function, below, to execute the steps required to produce the project's artifacts. 
###

set -o errexit  # Fail or exit immediately if there is an error.
set -o nounset  # Fail if an unset variable is used.
set -o pipefail # Fail pipelines if any command errors, not just the last one.

function ci-compose() {  
  function createDocs() {
    local sourcePath="${BUILD_UNPACKAGED_DIST}/net6.0/TestingUtils.Randomization.dll"
    local outputPath="${BUILD_DOCS}/md"
    
    cd "${PROJECT_ROOT}" &&
      dotnet tool restore &&
      dotnet xmldocmd "${sourcePath}" "${outputPath}" \
        --namespace "Jds.TestingUtils.Randomization" \
        --source "https://github.com/JeremiahSanders/testingutils-randomization/tree/main/src" \
        --newline lf \
        --visibility public
  }
  
  printf "Cleaning build artifacts from %s...\n\n" "${PROJECT_ROOT}/build" &&
    rm -rfv \
      "${PROJECT_ROOT}/build" &&
    printf "Composing build artifacts...\n\n" &&
    dotnet build "${PROJECT_ROOT}/src" \
      --configuration Release \
      -p:Version="${PROJECT_VERSION_DIST}" \
      -p:GenerateDocumentationFile=true &&
    printf "Publishing...\n\n" &&
    dotnet publish "${PROJECT_ROOT}/src" \
      --configuration Release \
      --output "${BUILD_UNPACKAGED_DIST}/net10.0" \
      -p:Version="${PROJECT_VERSION_DIST}" \
      -p:GenerateDocumentationFile=true \
      --framework net10.0 &&
    dotnet publish "${PROJECT_ROOT}/src" \
      --configuration Release \
      --output "${BUILD_UNPACKAGED_DIST}/net8.0" \
      -p:Version="${PROJECT_VERSION_DIST}" \
      -p:GenerateDocumentationFile=true \
      --framework net8.0 &&
    dotnet publish "${PROJECT_ROOT}/src" \
      --configuration Release \
      --output "${BUILD_UNPACKAGED_DIST}/net6.0" \
      -p:Version="${PROJECT_VERSION_DIST}" \
      -p:GenerateDocumentationFile=true \
      --framework net6.0 &&
    printf "\nPublish complete. Packing...\n\n" &&
    dotnet pack "${PROJECT_ROOT}/src" \
      --configuration Release \
      --output "${BUILD_PACKAGED_DIST}/nuget" \
      -p:PackageVersion="${PROJECT_VERSION_DIST}" \
      -p:Version="${PROJECT_VERSION_DIST}" \
      -p:GenerateDocumentationFile=true &&
    printf "\nPacking complete. Creating docs...\n\n" &&
    createDocs &&
    printf "Composition complete.\n"
}

export -f ci-compose
