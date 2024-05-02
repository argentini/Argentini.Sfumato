import fcntl
import os
import time

# Open the file
file_path = 'sfumato.yml'
file = open(file_path, 'r+')

try:
    # Try to acquire an exclusive lock on the file
    fcntl.flock(file.fileno(), fcntl.LOCK_EX | fcntl.LOCK_NB)
    print("File is locked.")

    # Specify the duration for which the lock should be held (in milliseconds)
    duration_ms = 10000  # 10000 milliseconds (10 seconds)

    # Wait for the specified duration
    time.sleep(duration_ms / 1000.0)  # Convert milliseconds to seconds for sleep

    print(f"Lock held for {duration_ms} milliseconds. Now releasing the lock.")

finally:
    # Release the lock
    fcntl.flock(file.fileno(), fcntl.LOCK_UN)
    file.close()
    print("File unlocked and closed.")
