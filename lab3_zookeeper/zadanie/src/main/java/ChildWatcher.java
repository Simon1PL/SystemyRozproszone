import org.apache.zookeeper.KeeperException;
import org.apache.zookeeper.WatchedEvent;
import org.apache.zookeeper.Watcher;
import org.apache.zookeeper.ZooKeeper;

public class ChildWatcher implements Watcher {
    private final ZooKeeper zk;
    private final String znode;

    public ChildWatcher(ZooKeeper zk, String znode) {
        this.zk = zk;
        this.znode = znode;
        printChildrenAmount();
    }

    @Override
    public void process(WatchedEvent event) {
        printChildrenAmount();
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