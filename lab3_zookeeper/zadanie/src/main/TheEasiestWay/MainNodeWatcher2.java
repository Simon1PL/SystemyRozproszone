import org.apache.zookeeper.*;
import java.io.IOException;

public class MainNodeWatcher2 implements Watcher {
    private final String exec;
    private final String znode;
    private final ZooKeeper zk;
    private Process process;

    public MainNodeWatcher2(String exec, ZooKeeper zk, String znode) throws KeeperException, InterruptedException, IOException {
        this.exec = exec;
        this.zk = zk;
        this.znode = znode;

        if (zk.exists(znode, false) != null) {
            process = Runtime.getRuntime().exec(exec);
        }
    }

    @Override
    public void process(WatchedEvent watchedEvent) {
        if(watchedEvent.getType() == Event.EventType.NodeCreated && watchedEvent.getPath().equals(znode)){
            try {
                process = Runtime.getRuntime().exec(exec);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        else if(watchedEvent.getType() == Event.EventType.NodeDeleted && watchedEvent.getPath().equals(znode)){
            process.destroy();
            System.out.println(znode + " deleted");
        }
        else {
            printChildrenAmount();
        }
    }

    private void printChildrenAmount() {
        try {
            if (zk.exists(znode, false) != null) {
                System.out.println("Children amount for " + znode + ": " + zk.getAllChildrenNumber(znode));
            }
        } catch (KeeperException | InterruptedException e) {
            e.printStackTrace();
        }
    }
}
