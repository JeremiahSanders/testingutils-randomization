#!/usr/bin/env bash

if [[ "${RELEASE_ENVIRONMENT:-false}" = true ]]; then
  export PROJECT_VERSION_DIST="${PROJECT_VERSION_DIST:-${PROJECT_VERSION}}"
else
  export PROJECT_VERSION_DIST="${PROJECT_VERSION}-build-$(TZ="utc" date "+%Y%m%d-%H%M%S")-sha-${CURRENT_GIT_HASH}"
fi
