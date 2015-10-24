# kfx-wfa-route-reject
Kofax Capture workflow agent that catches rejections and routes batches automatically.

## Overview
Kofax Capture doesn't have great out-of-the-box routing, so this workflow agent does some simple work to catch any rejections and route the batch automatically to a different module. You might want to use this if you don't want to use the Quality Control queue in your workflow.
