#!/usr/bin/env bash

if [[ "${RELEASE_ENVIRONMENT:-false}" = true ]]; then
  export PROJECT_VERSION_DIST="${PROJECT_VERSION_DIST:-${PROJECT_VERSION}}"
else
  # Calculate next version. Assume next version is a minor (so we'll use 0 instead of current patch version).
  IFS='.' read -ra PROJECT_VERSION_SEGMENTS <<< "${PROJECT_VERSION}"
  MAJOR="${PROJECT_VERSION_SEGMENTS[0]}"
  MINOR="${PROJECT_VERSION_SEGMENTS[1]}"
  PATCH="0"
  # The $(()) converts ${MINOR} to a number.
  BUMPED_MINOR=$((${MINOR}+1))
  export PROJECT_VERSION_DIST="${MAJOR}.${BUMPED_MINOR}.${PATCH}-build-$(TZ="utc" date "+%Y%m%d-%H%M%S")-sha-${CURRENT_GIT_HASH}"
fi
