package Z2;

import akka.actor.typed.ActorRef;
import akka.actor.typed.Behavior;
import akka.actor.typed.javadsl.AbstractBehavior;
import akka.actor.typed.javadsl.ActorContext;
import akka.actor.typed.javadsl.Behaviors;
import akka.actor.typed.javadsl.Receive;
import akka.actor.typed.receptionist.Receptionist;

import java.util.LinkedList;
import java.util.List;

public class ActorTextService extends AbstractBehavior<ActorTextService.Command>  {

    // --- messages
    interface Command {}

    // TODO: new message type implementing Command, with Receptionist.Listing field

    private static class ListingResponse implements Command {
        final Receptionist.Listing listing;

        private ListingResponse(Receptionist.Listing listing) {
            this.listing = listing;
        }
    }

    public static class Request implements Command {
        final String text;

        public Request(String text) {
            this.text = text;
        }
    }


    // --- constructor and create
    // TODO: field for message adapter
    private final ActorRef<Receptionist.Listing> listingResponseAdapter;
    private List<ActorRef<String>> workers = new LinkedList<>();

    public ActorTextService(ActorContext<ActorTextService.Command> context) {
        super(context);

        // TODO: create message adapter
        this.listingResponseAdapter =
                context.messageAdapter(Receptionist.Listing.class, ListingResponse::new);

        // TODO: subscribe to receptionist (with message adapter)
        context.getSystem().receptionist().tell(Receptionist.subscribe(ActorUpperCase.upperCaseServiceKey,listingResponseAdapter));
    }

    public static Behavior<Command> create() {
        return Behaviors.setup(ActorTextService::new);
    }

    // --- define message handlers
    @Override
    public Receive<Command> createReceive() {

        System.out.println("creating receive for text service");

        return newReceiveBuilder()
                .onMessage(Request.class, this::onRequest)
                .onMessage(ListingResponse.class, response -> onListing(response.listing))
                // TODO: handle the new type of message
                .build();
    }

    private Behavior<Command> onRequest(Request msg) {
        System.out.println("request: " + msg.text);
        for (ActorRef<String> worker : workers) {
            System.out.println("sending to worker: " + worker);
            worker.tell(msg.text);
        }
        return this;
    }

    // TODO: handle the new type of message
    private Behavior<Command> onListing(Receptionist.Listing msg) {
        workers = new LinkedList<>();
        workers.addAll(msg.getServiceInstances(ActorUpperCase.upperCaseServiceKey));
        return Behaviors.same();
    }
}